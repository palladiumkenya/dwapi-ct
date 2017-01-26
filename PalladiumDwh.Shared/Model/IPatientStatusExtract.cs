using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IPatientStatusExtract: IExtract,IStatus
    {
        Guid PatientId { get; set; }
    }
}