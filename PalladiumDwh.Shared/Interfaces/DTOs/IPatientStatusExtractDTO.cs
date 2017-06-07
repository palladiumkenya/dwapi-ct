using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientStatusExtractDTO: IExtractDTO,IStatus
    {
        Guid PatientId { get; set; }
    }
}