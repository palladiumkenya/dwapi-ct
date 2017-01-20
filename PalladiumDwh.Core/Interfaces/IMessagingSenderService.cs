namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingSenderService:IMessagingService
    {
        string Send(object message, string gateway = "");
    }
}