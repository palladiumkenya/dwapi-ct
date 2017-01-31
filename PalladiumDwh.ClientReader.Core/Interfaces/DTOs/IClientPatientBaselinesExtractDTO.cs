using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientPatientBaselinesExtractDTO: IClientExtractDTO,IBaseline
    {
        Guid PatientId { get; set; }
    }
}