using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientVisitExtract: IExtract,IVisit
    {
        Guid PatientId { get; set; }
    }
}