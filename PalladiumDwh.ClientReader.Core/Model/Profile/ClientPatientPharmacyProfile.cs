using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public class ClientPatientPharmacyProfile :ClientExtractProfile, IClientPatientPharmacyProfile
    {
        public List<ClientPatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<ClientPatientPharmacyExtractDTO>();
        public override string EndPoint { get; set; } = "PatientPharmacy";

        public static ClientPatientPharmacyProfile Create(ClientFacility facility, ClientPatientExtract patient)
        {
            var patientProfile = new ClientPatientPharmacyProfile
            {
                Facility = new ClientFacilityDTO(facility),
                Demographic = new ClientPatientExtractDTO(patient),
                PharmacyExtracts =
                    new ClientPatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(patient.ClientPatientPharmacyExtracts)
                        .ToList()
            };
            return patientProfile;
        }
    }
}