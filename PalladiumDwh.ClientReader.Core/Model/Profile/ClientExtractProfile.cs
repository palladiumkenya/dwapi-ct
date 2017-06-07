using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public abstract class ClientExtractProfile : IClientExtractProfile
    {
        public ClientFacilityDTO Facility { get; set; }
        public ClientPatientExtractDTO Demographic { get; set; }
        public virtual string EndPoint { get; set; }
        public virtual string Source { get; set; }
    }
}