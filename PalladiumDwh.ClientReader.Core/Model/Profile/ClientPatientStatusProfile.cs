using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public class ClientPatientStatusProfile :ClientExtractProfile, IClientPatientStatusProfile
    {
        public List<ClientPatientStatusExtractDTO> StatusExtracts { get; set; } = new List<ClientPatientStatusExtractDTO>();

        public static ClientPatientStatusProfile Create(ClientFacility facility, ClientPatientExtract patient)
        {
            var patientProfile = new ClientPatientStatusProfile
            {
                Facility = new ClientFacilityDTO(facility),
                Demographic = new ClientPatientExtractDTO(patient),
                StatusExtracts =
                    new ClientPatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(patient.ClientPatientStatusExtracts)
                        .ToList()
            };
            return patientProfile;
        }
    }
}