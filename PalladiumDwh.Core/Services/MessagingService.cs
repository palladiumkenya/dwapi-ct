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
    public class MessagingService : IMessagingService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private MessageQueue _queue;
        private readonly string _queueName;

        public string QueueName => _queueName;
        public object Queue => _queue;

        public MessagingService(string queueName)
        {
            _queueName = queueName;
        }

        public void Initialize()
        {
          
            try
            {
                if (!MessageQueue.Exists(_queueName))
                {
                    Log.Debug($"Initializing MessagingService [{_queueName}] ...");
                    _queue = MessageQueue.Create(_queueName, true);
                    Log.Debug($"MessagingService {_queueName} Initialized!");
                }
                else
                {
                    _queue = new MessageQueue(_queueName);
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"MessagingService error:!");
                Log.Debug(ex);
                throw;
            }
            
        }

        public string Send(object message)
        {
            string refId;

            if (null == _queue)
                Initialize();

            var messageToSend = CreateMessage(message);

            if (null != messageToSend && null != _queue)
            {
                try
                {
                    var tx = new MessageQueueTransaction();
                    tx.Begin();
                    _queue.Send(messageToSend, tx);
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
            Message msmqMessage = null;
            try
            {
                msmqMessage = new Message();
                var jsonBody = JsonConvert.SerializeObject(new LiveMessage(message, message.GetType().ToString()));
                msmqMessage.Label = $"{message}";
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