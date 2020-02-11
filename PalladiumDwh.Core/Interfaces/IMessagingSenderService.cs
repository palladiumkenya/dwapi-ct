using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IMessagingSenderService:IMessagingService
    {
        string Send(object message, string gateway = "");
        List<Guid> SendBatch(IEnumerable<dynamic>  message, string gateway = "");
        Task<string> SendAsync(object message, string gateway = "");
        Task<List<Guid>> SendBatchAsync(IEnumerable<dynamic>  messages, string gateway = "");
    }
}
