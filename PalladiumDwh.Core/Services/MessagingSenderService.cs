using System;
using System.Collections.Generic;
using System.Messaging;
using System.Threading.Tasks;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.Core.Services
{
    public class MessagingSenderService :MessagingService, IMessagingSenderService
    {
        public MessagingSenderService(string queueName) : base(queueName)
        {
        }

        public string Send(object message, string gateway = "")
        {
            string refId;

            if (null == Queue)
                Initialize(gateway);

            var msmq = Queue as MessageQueue;

            var messageToSend = CreateMessage(message);

            if (null != messageToSend && null != msmq)
            {
                try
                {
                    var tx = new MessageQueueTransaction();
                    tx.Begin();
                    msmq.Send(messageToSend, tx);
                    tx.Commit();
                    refId = messageToSend.Id;
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    throw;
                }
            }
            else
            {
                throw new Exception("Queue is not Initialized !");
            }

            return refId;
        }

        public List<Guid> SendBatch(IEnumerable<dynamic> messages, string gateway = "")
        {
            var list = new List<Guid>();

            foreach (var m in messages)
            {
                m.GeneratePatientRecord();
                string id = Send(m);
                list.Add(m.Id);
            }

            return list;
        }

        public Message CreateMessage(object message)
        {
            return Utility.CreateMessage(message);
        }

        public Task<string> SendAsync(object message, string gateway = "")
        {
            return Task.Run(() => Send(message, gateway));
        }

        public Task<List<Guid>> SendBatchAsync(IEnumerable<dynamic>  messages, string gateway = "")
        {
            return Task.Run(() => SendBatch(messages, gateway));
        }
    }
}
