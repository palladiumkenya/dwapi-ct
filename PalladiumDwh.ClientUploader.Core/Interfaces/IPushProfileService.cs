using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IPushProfileService
    {
        Task<string> SpotAsync(Manifest manifest);
        Task<PushResponse> PushAsync(IClientExtractProfile profile);
    }
}