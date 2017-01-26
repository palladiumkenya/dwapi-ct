using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IPatientLaboratoryExtractDTO: IExtractDTO,ILaboratory
    {
        Guid PatientId { get; set; }
    }
}