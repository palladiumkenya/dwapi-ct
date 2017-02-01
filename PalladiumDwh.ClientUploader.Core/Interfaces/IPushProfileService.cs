using System.Net.Http;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IPushProfileService
    {
        Task<HttpResponseMessage> PushAsync(IClientExtractProfile profile);
    }
}