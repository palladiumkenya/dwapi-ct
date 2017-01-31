using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientPatientExtractDTO : IClientExtractDTO, IPatient
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        string facilityName { get; set; }
        Guid FacilityId { get; set; }
    }
}