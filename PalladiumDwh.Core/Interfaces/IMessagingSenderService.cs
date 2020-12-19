using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingSenderService:IMessagingService
    {
        string Send(object message, string gateway = "", Type messageType = null);
        List<string> SendBatch(IEnumerable<dynamic>  message, string gateway = "");
        Task<string> SendAsync(object message, string gateway = "", Type messageType = null);
        Task<List<string>> SendBatchAsync(IEnumerable<dynamic>  messages, string gateway = "");
    }
}
