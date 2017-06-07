using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientBaselinesExtractDTO: IExtractDTO,IBaseline
    {
        Guid PatientId { get; set; }
    }
}