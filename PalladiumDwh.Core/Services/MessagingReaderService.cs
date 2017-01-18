using System;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Reflection;
using System.Text;
using log4net;
using Newtonsoft.Json;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model.Profiles;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Services
{
    public class MessagingReaderService : IMessagingReaderService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private MessageQueue _queue;
        private readonly ISyncService _syncService;
        private readonly string _queueName;
        private readonly int _batchSize;
        
        public string QueueName => _queueName;
        public object Queue => _queue;

        public MessagingReaderService(ISyncService syncService, string queueName, int batchSize = 50)
        {
            _queueName = queueName;
            _batchSize = batchSize;
            _syncService = syncService;
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

        public void Read()
        {
            int count = _queue.Count();
            if (count > 0)
            {
                var messageIds = _queue.GetAllMessages().Select(x => x.Id).ToList();
                Log.Debug($"Queue has {messageIds.Count} !");
                var batches = messageIds.Split(_batchSize).ToList();
                var batchCount = batches.Count;
                Log.Debug($"Queue will be processed in {batchCount} batches");
                int n = 0;
                foreach (var batch in batches)
                {
                    n++;
                    Log.Debug($"processing {n} of {batchCount} batches...");
                    foreach (var m in batch.ToList())
                    {
                        var msg = _queue.ReceiveById(m);
                        if (null != msg)
                        {
                            var patientProfile = msg.BodyStream.ReadFromJson<PatientARTProfile>();
                            _syncService.SyncArt(patientProfile);
                        }
                    }
                }
                Log.Debug(new string('*', 30));
            }
            else
            {
                Log.Debug("Nothing in queue!");
            }
        }

       
    }
}