using System;
using System.Messaging;
using System.Reflection;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Extentions;

namespace PalladiumDwh.Core.Services
{
    public abstract class MessagingService : IMessagingService
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _queueName;

        public virtual string QueueName { get; private set; }
        public string BacklogQueueName { get; private set; }
        public virtual object Queue { get; private set; }
        public virtual object BacklogQueue { get; private set; }

        protected MessagingService(string queueName)
        {
            _queueName = queueName;
        }

        public void Initialize(string gateway = "")
        {

            QueueName = string.IsNullOrWhiteSpace(gateway) ? $"{_queueName}" : $"{_queueName}.{gateway}";
            BacklogQueueName =  $"{QueueName}.backlog";

            try
            {
                Queue = GetQueue(QueueName);
            }
            catch (Exception ex)
            {
                Log.Debug($"MessagingService:{QueueName} error:!");
                Log.Debug(ex);
                throw;
            }
            try
            {
                BacklogQueue = GetQueue(BacklogQueueName);
            }
            catch (Exception ex)
            {
                Log.Debug($"MessagingService:{BacklogQueueName} error:!");
                Log.Debug(ex);
                throw;
            }
        }

        private MessageQueue GetQueue(string queueName)
        {
            MessageQueue queue;

            if (!MessageQueue.Exists(queueName))
            {
                Log.Debug($"Initializing MessagingService [{queueName}] ...");
                queue = MessageQueue.Create(queueName, true);
                Log.Debug($"MessagingService {queueName} Initialized!");
            }
            else
            {
                queue = new MessageQueue(queueName);
            }

            return queue;
        }

        public void Purge(string gateway,bool withJournal=false)
        {
            if (MessageQueue.Exists(gateway))
            {
                var queue = new MessageQueue(gateway);
                queue.Purge();
            }

            if(!withJournal)
                return;

            var jGateway = $@"{gateway}\Journal$";

            if (MessageQueue.Exists(jGateway))
            {
                var queue = new MessageQueue(jGateway);
                queue.Purge();
            }
        }

        public int GetNumberOfMessages(string gateway, bool isJournal=false)
        {
            
            if (isJournal)
                gateway= $@"{gateway}\Journal$";

            if (MessageQueue.Exists(gateway))
            {
                var queue = new MessageQueue(gateway);
                return queue.Count();
            }
            return -1;
        }

        public void Delete(string gateway)
        {
            if (MessageQueue.Exists(gateway))
            {
                MessageQueue.Delete(gateway);
            }
        }
    }
}