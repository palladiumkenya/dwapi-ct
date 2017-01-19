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
    public class MessagingReaderService : MessagingService, IMessagingReaderService
    {
        private readonly ISyncService _syncService;
        private readonly int _batchSize;

        public MessagingReaderService(ISyncService syncService, string queueName, int batchSize = 50) : base(queueName)
        {
            _syncService = syncService;
            _batchSize = batchSize;
        }

        public void Read()
        {
            if (null == Queue)
                Initialize();

            var msmq = Queue as MessageQueue;

            if (null == msmq)
            {
                return;
            }

            var count = msmq.Count();

            if (count > 0)
            {
                var messageIds = msmq.GetAllMessages().Select(x => x.Id).ToList();

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
                        var msg = msmq.ReceiveById(m);
                        if (null != msg)
                        {

                            var patientProfile = msg.BodyStream.ReadFromJson(msg.Label);
                            switch (patientProfile.GetType()==typeof(PatientARTProfile))
                            {
                            _syncService.SyncArt(patientProfile);   
                        }
                            
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