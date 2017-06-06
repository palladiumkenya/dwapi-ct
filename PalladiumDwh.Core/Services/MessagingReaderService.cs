using System;
using System.Linq;
using System.Messaging;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Extentions;

namespace PalladiumDwh.Core.Services
{
    public class MessagingReaderService : MessagingService, IMessagingReaderService
    {
        private readonly ISyncService _syncService;
        private readonly int _queueBatch;

        public MessagingReaderService(ISyncService syncService, string queueName, int queueBatch = 50) : base(queueName)
        {
            _syncService = syncService;
            _queueBatch = queueBatch;
        }

        public void Read(string gateway)
        {
            if (null == Queue)
                Initialize(gateway);

            var msmq = Queue as MessageQueue;

            if (null == msmq)
            {
                return;
            }

            var count = msmq.Count();

            if (count > 0)
            {
                var messageIds = msmq.GetAllMessages().Select(x => x.Id).ToList();

                Log.Debug($"Queue {QueueName} has {messageIds.Count} !");

                var batches = messageIds.Split(_queueBatch).ToList();
                var batchCount = batches.Count;

                Log.Debug($"Queue {QueueName} will be processed in {batchCount} batches");

                int n = 0;
                foreach (var batch in batches)
                {
                    n++;
                    Log.Debug($"processing {QueueName} {n} of {batchCount} batches...");
                    foreach (var m in batch.ToList())
                    {

                        var msg = msmq.ReceiveById(m);
                        if (null != msg)
                        {
                            try
                            {
                                var patientProfile = msg.BodyStream.ReadFromJson(msg.Label);
                                _syncService.Sync(patientProfile);
                            }
                            catch (Exception e)
                            {
                                Log.Debug(e);
                                MoveToBacklog(msg);
                            }

                        }
                    }
                }
                Log.Debug(new string('*', 30));
            }
        }

        public void MoveToBacklog(object message)
        {
            var msmq=BacklogQueue as MessageQueue;

            if (null != message && null != msmq)
            {
                var tx = new MessageQueueTransaction();
                tx.Begin();
                msmq.Send(message, tx);
                tx.Commit();
            }
        }

        public void PrcocessBacklog(string gateway="")
        {
            if (null == Queue)
                Initialize(gateway);

            var msmqMain = Queue as MessageQueue;

            if (null == msmqMain)
            {
                return;
            }


            if (null == BacklogQueue)
                Initialize(gateway);

            var msmqBacklog = BacklogQueue as MessageQueue;

            if (null == msmqBacklog)
            {
                return;
            }           

            var count = msmqBacklog.Count();

            if (count > 0)
            {
                var messageIds = msmqBacklog.GetAllMessages().Select(x => x.Id).ToList();

                Log.Debug($"Backlog-Queue {QueueName} has {messageIds.Count} !");

                var batches = messageIds.Split(_queueBatch).ToList();
                var batchCount = batches.Count;

                Log.Debug($"Backlog-Queue {QueueName} will be processed in {batchCount} batches");

                int n = 0;
                foreach (var batch in batches)
                {
                    n++;
                    Log.Debug($"processing {QueueName} {n} of {batchCount} batches...");
                    foreach (var m in batch.ToList())
                    {
                        var msg = msmqBacklog.PeekById(m);
                        if (null != msg)
                        {
                            try
                            {
                                var tx = new MessageQueueTransaction();
                                tx.Begin();
                                msmqMain.Send(msg, tx);
                                tx.Commit();
                                msmqBacklog.ReceiveById(m);
                            }
                            catch (Exception e)
                            {
                                Log.Debug(e);
                            }
                        }
                    }
                }
                Log.Debug(new string('*', 30));
            }
        }
    }
}