using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Model
{
    public class SiteProfile
    {
        public Manifest Manifest { get; set; }
        public List<ClientPatientExtract> ClientPatientExtracts { get; set; } = new List<ClientPatientExtract>();

        private SiteProfile(Manifest manifest)
        {
            Manifest = manifest;
        }

        public static List<SiteProfile> Create(SiteManifest siteManifest)
        {
            var siteProfiles=new List<SiteProfile>();

            foreach (var manifest in siteManifest.Manifests)
            {
                var siteProfile = new SiteProfile(manifest);
                var patientExtracts = siteManifest.PatientExtracts.Where(x => x.SiteCode == manifest.SiteCode).ToList();
                if (null != patientExtracts)
                {
                    if (patientExtracts.Count > 0)
                        siteProfile.ClientPatientExtracts.AddRange(patientExtracts);
                }
                siteProfiles.Add(siteProfile);
            }

            return siteProfiles;
        }
    }
}