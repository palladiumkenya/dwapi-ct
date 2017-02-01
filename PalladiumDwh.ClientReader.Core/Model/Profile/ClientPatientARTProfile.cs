using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public class ClientPatientARTProfile :ClientExtractProfile, IClientPatientARTProfile
    {
        public List<ClientPatientArtExtractDTO> ArtExtracts { get; set; } = new List<ClientPatientArtExtractDTO>();
        public override string EndPoint { get; set; } = "PatientArt";

        public static ClientPatientARTProfile Create(ClientFacility facility, ClientPatientExtract patient)
        {
            var patientProfile = new ClientPatientARTProfile
            {
                Facility = new ClientFacilityDTO(facility),
                Demographic = new ClientPatientExtractDTO(patient),
                ArtExtracts =
                    new ClientPatientArtExtractDTO().GeneratePatientArtExtractDtOs(patient.ClientPatientArtExtracts).ToList()
            };
            return patientProfile;
        }
    }
}