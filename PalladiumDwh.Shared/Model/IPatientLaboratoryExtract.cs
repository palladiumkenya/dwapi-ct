using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IPatientLaboratoryExtract: IExtract,ILaboratory
    {
        Guid PatientId { get; set; }
    }
}