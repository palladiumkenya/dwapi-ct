using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IPatientVisitExtract: IExtract,IVisit
    {
        Guid PatientId { get; set; }
    }
}