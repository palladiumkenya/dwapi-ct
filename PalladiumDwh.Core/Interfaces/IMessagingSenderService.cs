using System.Threading.Tasks;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingSenderService:IMessagingService
    {
        string Send(object message, string gateway = "");
        Task<string> SendAsync(object message, string gateway = "");
    }
}