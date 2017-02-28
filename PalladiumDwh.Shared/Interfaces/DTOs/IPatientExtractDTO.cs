using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientExtractDTO : IExtractDTO, IPatient
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        Guid FacilityId { get; set; }
    }
}