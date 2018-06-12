using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PalladiumDwh.DWapi.Models
{
    public class Subscriber
    {
        public string SubscriberId { get; set; }
        public string AuthToken { get; set; }

        public Subscriber()
        {
        }

        public bool Verify()
        {
            return SubscriberId.ToUpper() == "DWAPI" &&
                   AuthToken.ToLower() == "1ba47c2a-6e05-11e8-adc0-fa7ae01bbebc".ToLower();
        }
    }
}