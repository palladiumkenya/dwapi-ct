using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IProfileManager
    {
        ClientPatientExtract Decode(string encodedPatient);
        IEnumerable<IClientExtractProfile> Generate(ClientPatientExtract patient);
        IEnumerable<SiteProfile> GenerateSiteProfiles(SiteManifest siteManifest);
    }
}