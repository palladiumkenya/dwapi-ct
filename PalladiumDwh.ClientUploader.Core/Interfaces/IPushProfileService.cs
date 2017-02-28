using System.Net.Http;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IPushProfileService
    {
        Task<PushResponse> PushAsync(IClientExtractProfile profile);
    }
}