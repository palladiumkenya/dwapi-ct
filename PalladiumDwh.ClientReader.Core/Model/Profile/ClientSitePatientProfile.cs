using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public class ClientSitePatientProfile 
    {
        public Manifest Manifest { get; set; }
        public ClientFacilityDTO Facility { get; set; }
        public ClientPatientExtractDTO Demographic { get; set; }
        public List<ClientPatientArtExtractDTO> ArtExtracts { get; set; } = new List<ClientPatientArtExtractDTO>();
        public List<ClientPatientVisitExtractDTO> VisitExtracts { get; set; } = new List<ClientPatientVisitExtractDTO>();
    }
}