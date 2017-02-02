using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Model.Profile
{
    public class ClientPatientLabProfile : ClientExtractProfile, IClientPatientLabProfile
    {
        public List<ClientPatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } =new List<ClientPatientLaboratoryExtractDTO>();
        public override string EndPoint { get; set; } = "PatientLabs";
        public static ClientPatientLabProfile Create(ClientFacility facility, ClientPatientExtract patient)
        {
            var patientProfile = new ClientPatientLabProfile
            {
                Facility = new ClientFacilityDTO(facility),
                Demographic = new ClientPatientExtractDTO(patient),
                LaboratoryExtracts =
                    new ClientPatientLaboratoryExtractDTO().GenerateLaboratoryExtractDtOs(patient.ClientPatientLaboratoryExtracts)
                        .ToList()
            };
            return patientProfile;
        }
    }
}