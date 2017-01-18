using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.Shared
{
    public class LiveMessage
    {
        public object MessageItem { get; set; }
        public string MessageType { get; set; }

        public LiveMessage(object messageItem, string messageType)
        {
            MessageItem = messageItem;
            MessageType = messageType;
        }
    }
}
