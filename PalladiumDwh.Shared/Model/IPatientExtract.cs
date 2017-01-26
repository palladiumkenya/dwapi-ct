using System;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IPatientExtract : IExtract, IPatient
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        Guid FacilityId { get; set; }
    }
}