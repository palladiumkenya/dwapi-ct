using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IPatientVisitExtractDTO: IExtractDTO,IVisit
    {
        Guid PatientId { get; set; }
    }
}