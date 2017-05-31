using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Model
{
    public class ExportsViewModel
    {
        public int SiteCode { get; set; }
        public int Records { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }

        private ExportsViewModel(int siteCode, int records, string location)
        {
            SiteCode = siteCode;
            Records = records;
            Location = location;
        }

        public static List<ExportsViewModel> Create(SiteManifest siteManifest)
        {
            var list = new List<ExportsViewModel>();
            foreach (var site in siteManifest.Manifests)
            {
                list.Add(new ExportsViewModel(site.SiteCode,site.PatientPKs.Count, siteManifest.Location));
            }
            return list;
        }
    }
}