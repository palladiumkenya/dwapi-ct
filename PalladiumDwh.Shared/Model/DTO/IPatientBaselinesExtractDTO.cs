using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IPatientBaselinesExtractDTO: IExtractDTO,IBaseline
    {
        Guid PatientId { get; set; }
    }
}