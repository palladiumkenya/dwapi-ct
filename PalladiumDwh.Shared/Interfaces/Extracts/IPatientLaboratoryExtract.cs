using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientLaboratoryExtract: IExtract,ILaboratory
    {
        Guid PatientId { get; set; }
    }
}