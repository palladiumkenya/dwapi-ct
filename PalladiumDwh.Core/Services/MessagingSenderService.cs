using System;
using System.Collections.Generic;
using System.Messaging;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.Core.Services
{
    public class MessagingSenderService :MessagingService, IMessagingSenderService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MessagingSenderService(string queueName) : base(queueName)
        {
        }

        public string Send(object message, string gateway = "",Type messageType = null)
        {
            string refId;

            if (null == Queue)
                Initialize(gateway);

            var msmq = Queue as MessageQueue;

            Message messageToSend = null;

            messageToSend = CreateMessage(message,messageType);

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
                    Log.Error("Send Error",ex);
                    throw;
                }
            }
            else
            {
                throw new Exception("Queue is not Initialized !");
            }

            return refId;
        }

        public List<string> SendBatch(IEnumerable<dynamic> messages, string gateway = "")
        {
            var list = new List<string> {Guid.NewGuid().ToString()};
            try
            {
                SendBatchMessages(messages);
            }
            catch (Exception ex)
            {
                Log.Error($"Batch POST Error...");
                Log.Error(ex.Message);
                throw;
            }

            return list;
        }

        public void SendBatchMessages(IEnumerable<dynamic> messages)
        {
            foreach (var m in messages)
            {
                m.GeneratePatientRecord();
                string id = Send(m);
            }
        }

        public Message CreateMessage(object message,Type messageType=null)
        {
            return Utility.CreateMessage(message,messageType);
        }

        public Task<string> SendAsync(object message, string gateway = "", Type messageType = null)
        {
            return Task.Run(() => Send(message, gateway,messageType));
        }

        public Task<List<string>> SendBatchAsync(IEnumerable<dynamic>  messages, string gateway = "")
        {
            return Task.Run(() => SendBatch(messages, gateway));
        }
    }
}
