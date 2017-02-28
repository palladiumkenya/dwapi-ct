using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientLaboratoryExtractDTO: IExtractDTO,ILaboratory
    {
        Guid PatientId { get; set; }
    }
}