using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{

    public class DiscoverSource
    {
        public int? SiteCode { get; set;}
        public int? NumOfTempRecords { get; set; }

        public DiscoverSource()
        {
        }

        

        public override string ToString()
        {
            var siteCode = SiteCode.HasValue ? SiteCode.ToString() : "";
            var num= NumOfTempRecords.HasValue ? NumOfTempRecords.ToString() : "";

            return $"Sites:{siteCode} | Nums:{num}";
        }
    }
}
