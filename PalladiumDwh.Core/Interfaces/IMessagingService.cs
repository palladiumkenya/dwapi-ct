﻿namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingService
    {
        string QueueName { get; }
        string BacklogQueueName { get; }
        string BacklogDeadQueueName { get; }
        object Queue { get; }
        object BacklogQueue { get; }
        object BacklogDeadQueue { get; }
        void Initialize(string gateway = "");
        void Purge(string gateway, bool withJournal=false);
        void Delete(string gateway);
        int GetNumberOfMessages(string gateway, bool isJournal=false);
    }
}
