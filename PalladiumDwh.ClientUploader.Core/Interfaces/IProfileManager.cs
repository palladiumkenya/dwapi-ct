using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Profile;
using PalladiumDwh.ClientUploader.Core.Model;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IProfileManager
    {
        ClientPatientExtract Decode(string encodedPatient);
        IEnumerable<IClientExtractProfile> Generate(ClientPatientExtract patient);
        IEnumerable<SiteProfile> GenerateSiteProfiles(SiteManifest siteManifest);
        ClientSitePatientProfile SitePatientProfile(SiteProfile siteProfile);
    }
}