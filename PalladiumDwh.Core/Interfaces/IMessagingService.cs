using System.Collections.Generic;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingService
    {
        string QueueName { get; }
        object Queue { get; }
        void Initialize(string gateway = "");
        void Purge(string gateway, bool withJournal=false);
        int GetNumberOfMessages(string gateway, bool isJournal=false);
    }
}
