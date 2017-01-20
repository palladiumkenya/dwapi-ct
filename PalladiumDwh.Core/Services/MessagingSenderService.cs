using System;
using System.IO;
using System.Messaging;
using System.Reflection;
using System.Text;
using log4net;
using Newtonsoft.Json;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared;

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

        private Message CreateMessage(object message)
        {
            Message msmqMessage;

            try
            {
                msmqMessage = new Message();
                msmqMessage.Label = Utility.GetMessageType(message.GetType());
                var jsonBody = JsonConvert.SerializeObject(message);
                msmqMessage.BodyStream = new MemoryStream(Encoding.Default.GetBytes(jsonBody));
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                throw;
            }
           
            return msmqMessage;
        }
    }
}