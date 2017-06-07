using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientStatusExtract: IExtract,IStatus
    {
        Guid PatientId { get; set; }
    }
}