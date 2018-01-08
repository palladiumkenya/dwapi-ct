namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingReaderService:IMessagingService
    {
        void Read(string gateway="");
        void ExpressRead(string gateway = "");
        void MoveToBacklog(object message);
        void PrcocessBacklog(string gateway="");
    }
}