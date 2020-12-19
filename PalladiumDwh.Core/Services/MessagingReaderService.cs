using System;
using System.Collections.Generic;
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
            //initialize Queue

            #region Queue Init

            if (null == Queue)
                Initialize(gateway);

            var msmq = Queue as MessageQueue;

            if (null == msmq)
                return;

            #endregion

            //check if Queue has messages

            #region Queue check

            Message peekMessage = null;
            try
            {
                peekMessage = msmq.Peek(new TimeSpan(0));
            }
            catch (Exception e)
            {
                Log.Debug(e);
            }

            if (null == peekMessage)
            {
                return;
            }

            #endregion

            //process Queue

            Log.Debug($"Queue {QueueName} getting message Ids...");
            var messageIds = msmq.GetIds();

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

        public void ExpressRead(string gateway = "")
        {
            //initialize Queue
            bool isBatch = gateway.EndsWith(".batch");
            #region Queue Init

            if (null == Queue)
                Initialize(gateway);

            var msmq = Queue as MessageQueue;

            if (null == msmq)
                return;

            #endregion

            //check if Queue has messages

            #region Queue check

            Message peekMessage = null;
            try
            {
                peekMessage = msmq.Peek(new TimeSpan(0));
            }
            catch (MessageQueueException ex)
            {

            }
            catch (Exception e)
            {
                Log.Debug($"Queue {QueueName} peek error!");
                Log.Debug(e);
            }

            if (null == peekMessage)
            {
                return;
            }

            #endregion

            //process Queue

            Log.Debug($"Queue {QueueName} processing...");

            int count = 0;
            int batchCount = 0;
            var messages = new List<Message>();
            var messageEnumerator = msmq.GetMessageEnumerator2();

            //initialize Sync
            _syncService.InitList(QueueName);

            while (messageEnumerator.MoveNext())
            {
                if (null != messageEnumerator.Current)
                {
                    #region Read Message

                    Message message = null;
                    string messageId = messageEnumerator.Current.Id;
                    try
                    {
                        message = msmq.ReceiveById(messageId, new TimeSpan(0));
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Queue {QueueName} Message {messageId} Not Found...");
                        Log.Debug(e);
                    }

                    #endregion

                    #region Proccess Message

                    if (null != message)
                    {
                        count++;
                        batchCount++;

                        messages.Add(message);

                        try
                        {
                            var patientProfile = message.BodyStream.ReadFromJson(message.Label);
                            _syncService.Sync(patientProfile);

                            #region Batch Sync profiles

                            try
                            {
                                if (batchCount == 1000 || isBatch)
                                {
                                    // reset batch Count
                                    batchCount = 0;

                                    // commit sync
                                    _syncService.Commit(QueueName);

                                    // reset sync
                                    _syncService.InitList(QueueName);
                                    messages = new List<Message>();
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Debug("moving to Batch >> BACKLOG caused by Error:");
                                Log.Debug(e);
                                MoveToBacklog(messages);

                                // reset sync
                                _syncService.InitList(QueueName);
                                messages = new List<Message>();
                            }

                            #endregion

                        }
                        catch (Exception e)
                        {
                            Log.Debug("moving to BACKLOG caused by Error:");
                            Log.Debug(e);
                            MoveToBacklog(message);

                            // remove bad message from batch
                            messages.Remove(message);
                        }
                    }

                    #endregion
                }
            }

            #region Process last Messages

            try
            {
                // commit sync
                _syncService.Commit(QueueName);

                // reset sync
                _syncService.InitList(QueueName);
                messages = new List<Message>();
            }
            catch (Exception e)
            {
                Log.Debug("moving to BACKLOG caused by Error:");
                Log.Debug(e);
                MoveToBacklog(messages);
            }

            #endregion

            msmq.Close();

            Log.Debug($"Queue {QueueName} processed {count}");
            Log.Debug(new string('*', 30));
        }

        public void MoveToBacklog(object message)
        {
            var messages = new List<Message> {message as Message};
            MoveToBacklog(messages);
        }

        public void MoveToBacklog(List<Message> messages)
        {
            if(null==messages)
                return;
            if (messages.Count==0)
                return;

            var msmq = BacklogQueue as MessageQueue;
            if (null != msmq)
            {
                var tx = new MessageQueueTransaction();
                tx.Begin();
                foreach (var message in messages)
                {
                    if (null != message)
                    {
                        msmq.Send(message, tx);
                    }
                }
                tx.Commit();
                msmq.Close();
            }
        }

        public void MoveToBacklogDead(object message)
        {
            var msmq = BacklogDeadQueue as MessageQueue;

            if (null != message && null != msmq)
            {
                var tx = new MessageQueueTransaction();
                tx.Begin();
                msmq.Send(message, tx);
                tx.Commit();
                msmq.Close();
            }
        }

        public void PrcocessBacklog(string gateway = "")
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
                var messageIds = msmqBacklog.GetIds();

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

        public void ExpressPrcocessBacklog(string gateway = "")
        {
            //initialize Queue
            bool isBatch = gateway.EndsWith(".batch");

            #region Queue Init

            if (null == BacklogQueue)
                Initialize(gateway);

            var msmq = BacklogQueue as MessageQueue;

            if (null == msmq)
                return;

            #endregion

            //check if Queue has messages

            #region Queue check

            Message peekMessage = null;
            try
            {
                peekMessage = msmq.Peek(new TimeSpan(0));
            }
            catch (MessageQueueException ex)
            {
            }
            catch (Exception e)
            {
                Log.Debug($"Queue {QueueName} peek error!");
                Log.Debug(e);
            }

            if (null == peekMessage)
            {
                return;
            }

            #endregion

            //process Queue

            Log.Debug($"Queue {QueueName} processing...");

            int count = 0;
            var messages = new List<Message>();
            var messageEnumerator = msmq.GetMessageEnumerator2();

            while (messageEnumerator.MoveNext())
            {
                if (null != messageEnumerator.Current)
                {
                    #region Read Message

                    Message message = null;
                    string messageId = messageEnumerator.Current.Id;
                    try
                    {
                        message = msmq.ReceiveById(messageId, new TimeSpan(0));
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Queue {QueueName} Message {messageId} Not Found...");
                        Log.Debug(e);
                    }

                    #endregion

                    #region Proccess Message

                    if (null != message)
                    {
                        count++;

                        messages.Add(message);

                        try
                        {
                            var patientProfile = message.BodyStream.ReadFromJson(message.Label);
                            _syncService.Sync(patientProfile);
                        }
                        catch (Exception e)
                        {
                            Log.Debug("moving to DEAD BACKLOG caused by Error:");
                            Log.Debug(e);
                            MoveToBacklogDead(message);

                            // remove bad message from batch
                            messages.Remove(message);
                        }
                    }

                    #endregion
                }
            }

            msmq.Close();

            Log.Debug($"Queue {QueueName} processed {count}");
            Log.Debug(new string('*', 30));
        }
    }
}
