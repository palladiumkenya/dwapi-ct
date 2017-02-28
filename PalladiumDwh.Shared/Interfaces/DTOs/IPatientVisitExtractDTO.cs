using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientVisitExtractDTO: IExtractDTO,IVisit
    {
        Guid PatientId { get; set; }
    }
}