using System.Collections.Generic;
using System.Threading.Tasks;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingSenderService:IMessagingService
    {
        string Send(object message, string gateway = "");
        List<string> SendBatch(IEnumerable<dynamic>  message, string gateway = "");
        Task<string> SendAsync(object message, string gateway = "");
        Task<List<string>> SendBatchAsync(IEnumerable<dynamic>  messages, string gateway = "");
    }
}
