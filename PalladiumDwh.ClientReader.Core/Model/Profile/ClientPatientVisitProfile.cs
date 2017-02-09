using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public class ClientPatientVisitProfile :ClientExtractProfile, IClientPatientVisitProfile
    {
        public List<ClientPatientVisitExtractDTO> VisitExtracts { get; set; } = new List<ClientPatientVisitExtractDTO>();
        public override string EndPoint { get; set; } = "PatientVisits";
        public override string Source { get; set; } = "PatientVisitExtract";

        public static ClientPatientVisitProfile Create(ClientFacility facility, ClientPatientExtract patient)
        {
            var patientProfile = new ClientPatientVisitProfile
            {
                Facility = new ClientFacilityDTO(facility),
                Demographic = new ClientPatientExtractDTO(patient),
                VisitExtracts =
                    new ClientPatientVisitExtractDTO().GeneratePatientVisitExtractDtOs(patient.ClientPatientVisitExtracts).ToList()
            };
            return patientProfile;
        }
    }
}