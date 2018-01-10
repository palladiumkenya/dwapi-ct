using System.Collections.Generic;
using System.Messaging;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingReaderService:IMessagingService
    {
        void Read(string gateway="");
        void ExpressRead(string gateway = "");
        void MoveToBacklog(object message);
        void MoveToBacklog(List<Message> messages);
        void MoveToBacklogDead(object message);
        void PrcocessBacklog(string gateway="");
        void ExpressPrcocessBacklog(string gateway = "");
    }
}