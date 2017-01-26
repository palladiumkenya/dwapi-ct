using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IPatientExtractDTO : IExtractDTO, IPatient
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        Guid FacilityId { get; set; }
    }
}