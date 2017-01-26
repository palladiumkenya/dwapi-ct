using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IPatientBaselinesExtract: IExtract,IBaseline
    {
        Guid PatientId { get; set; }
    }
}