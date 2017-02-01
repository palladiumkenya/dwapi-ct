using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IPushProfileService
    {
        bool Push(IClientExtractProfile profile);
    }
}