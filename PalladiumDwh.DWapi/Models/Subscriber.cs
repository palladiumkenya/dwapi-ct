using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PalladiumDwh.DWapi.Models
{
    /// <summary>
    /// Subscriber
    /// </summary>
    /// <remarks>
    /// Regisistered client allowed to send data to the NDWH
    /// </remarks>
    public class Subscriber
    {
        /// <summary>
        /// SubscriberId as identified in the NDWH Subscriber Registry
        /// </summary>

        public string SubscriberId { get; set; }
        /// <summary>
        /// Token issued by NDWH Subscriber Registry
        /// </summary>

        public string AuthToken { get; set; }

        public Subscriber()
        {
        }

        public Subscriber(string subscriberId, string authToken)
        {
            SubscriberId = subscriberId;
            AuthToken = authToken;
        }

        public bool Verify()
        {
            return CoreSubscribers().Any(
                x => x.SubscriberId.ToUpper() == SubscriberId &&
                     x.AuthToken.ToLower() == AuthToken.ToLower());
        }

        public List<Subscriber> CoreSubscribers()
        {
            return new List<Subscriber>
            {
                new Subscriber("DWAPI","1ba47c2a-6e05-11e8-adc0-fa7ae01bbebc"),
                new Subscriber("AMRS","6d7c7224-m26b-11a8-8un2-f2801f1b9fd1")
            };
        }
    }
}