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
            if (null == Queue)
                Initialize(gateway);

            var msmq = Queue as MessageQueue;

            if (null == msmq)
            {
                return;
            }

         Log.Debug($"Queue {QueueName} checking for messages...");
      //var count = msmq.Count();
          Message peekMessage = null;
          try
          {
            peekMessage = msmq.Peek(new TimeSpan(0));
          }
          catch 
          {
            
          }

          if (null == peekMessage)
          {
            Log.Debug($"Queue {QueueName} 0 messages found !");
      }


          if (null!=peekMessage)
            {
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
        }

       public void ExpressRead(string gateway = "")
       {
          if (null == Queue)
             Initialize(gateway);

          var msmq = Queue as MessageQueue;

          if (null == msmq)
          {
             return;
          }

          Log.Debug($"Queue {QueueName} checking for messages...");
          Message peekMessage = null;
          try
          {
             peekMessage = msmq.Peek(new TimeSpan(0));
          }
          catch
          {
          }

          if (null == peekMessage)
          {
             Log.Debug($"Queue {QueueName} 0 messages found !");
             return;
          }

          Log.Debug($"Queue {QueueName} processing...");
          var enumerator = msmq.GetMessageEnumerator2();
          int n = 0;
          int count = 0;
          _syncService.InitList(QueueName);
          var msgs = new List<Message>();
          while (enumerator.MoveNext())
          {
             if (null != enumerator.Current)
             {
                var msg = msmq.ReceiveById(enumerator.Current.Id);
                n++;
                count++;
                try
                {
                   msgs.Add(msg);
                   var patientProfile = msg.BodyStream.ReadFromJson(msg.Label);

                   _syncService.Sync(patientProfile);
                   try
                   {
                      if (count == 1000)
                      {
                         _syncService.Commit(QueueName);
                         _syncService.InitList(QueueName);
                         msgs = new List<Message>();
                     }
                   }
                   catch (Exception e)
                  {
                     Log.Debug("moving to BACKLOG caused by Error:");
                     Log.Debug(e);
                      MoveToBacklog(msgs);
                   }
                }
                catch (Exception e)
               {
                  Log.Debug("moving to BACKLOG caused by Error:");
                  Log.Debug(e);
                   MoveToBacklog(msg);
                }
                if (count == 1000)
                {
                   Log.Debug($"Queue {QueueName} still processing, {n} so far...");
                   count = 0;
                }
             }

          }
          try
         {
            _syncService.Commit(QueueName);
            _syncService.InitList(QueueName);
            msgs=new List<Message>();

         }
          catch (Exception e)
          {
            Log.Debug("moving to BACKLOG caused by Error:");
            Log.Debug(e);
             MoveToBacklog(msgs);
          }
      

          Log.Debug($"Queue {QueueName} processed {n}");
          Log.Debug(new string('*', 30));
       }

       public void MoveToBacklog(List<Message> messages)
       {
          var msmq = BacklogQueue as MessageQueue;
          foreach (var message in messages)
          {
            if (null != message && null != msmq)
            {
               var tx = new MessageQueueTransaction();
               tx.Begin();
               msmq.Send(message, tx);
               tx.Commit();
            }
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
    }
}