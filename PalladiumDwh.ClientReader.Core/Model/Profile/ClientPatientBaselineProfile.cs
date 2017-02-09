using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public class ClientPatientBaselineProfile :ClientExtractProfile , IClientPatientBaselineProfile
    {
        public List<ClientPatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<ClientPatientBaselinesExtractDTO>();
        public override string EndPoint { get; set; } = "PatientBaselines";
        public override string Source { get; set; } = "PatientBaselinesExtract";


        public static ClientPatientBaselineProfile Create(ClientFacility facility, ClientPatientExtract patient)
        {
            var patientProfile = new ClientPatientBaselineProfile
            {
                Facility = new ClientFacilityDTO(facility),
                Demographic = new ClientPatientExtractDTO(patient),
                BaselinesExtracts =
                    new ClientPatientBaselinesExtractDTO().GeneratePatientBaselinesExtractDtOs(
                        patient.ClientPatientBaselinesExtracts).ToList()
            };
            return patientProfile;
        }
    }
}