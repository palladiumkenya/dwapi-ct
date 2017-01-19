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
    public abstract class MessagingService: IMessagingService
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _queueName;

        public virtual string QueueName { get; private set; }
        public virtual object Queue { get; private set; }

        protected MessagingService(string queueName)
        {
            _queueName = queueName;
        }

        public void Initialize(string gateway = "")
        {
            
            QueueName = string.IsNullOrWhiteSpace(gateway)? $"{_queueName}": $"{_queueName}.{gateway}";

            try
            {
                if (!MessageQueue.Exists(QueueName))
                {
                    Log.Debug($"Initializing MessagingService [{QueueName}] ...");
                    Queue = MessageQueue.Create(QueueName, true);
                    Log.Debug($"MessagingService {QueueName} Initialized!");
                }
                else
                {
                    Queue = new MessageQueue(QueueName);
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"MessagingService:{QueueName} error:!");
                Log.Debug(ex);
                throw;
            }
        }
    }
}