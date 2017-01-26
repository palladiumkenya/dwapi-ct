using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IPatientStatusExtractDTO: IExtractDTO,IStatus
    {
        Guid PatientId { get; set; }
    }
}