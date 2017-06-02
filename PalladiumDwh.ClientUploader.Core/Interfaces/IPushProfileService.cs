using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IPushProfileService
    {
        Task<string> SpotAsync(Manifest manifest, IProgress<DProgress> progress = null);

        Task<PushResponse> PushAsync(IClientExtractProfile profile);
        Task<PushResponse> PushAsync(SiteProfile siteProfile);
    }
}