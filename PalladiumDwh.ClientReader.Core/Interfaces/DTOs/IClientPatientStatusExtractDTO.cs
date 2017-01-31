using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientPatientStatusExtractDTO: IClientExtractDTO,IStatus
    {
        Guid PatientId { get; set; }
    }
}