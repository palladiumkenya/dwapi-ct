using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientExtract : IExtract, IPatient
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        Guid FacilityId { get; set; }
    }
}