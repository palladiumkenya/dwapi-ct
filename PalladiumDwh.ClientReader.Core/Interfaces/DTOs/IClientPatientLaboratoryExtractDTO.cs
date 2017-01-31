using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientPatientLaboratoryExtractDTO: IClientExtractDTO,ILaboratory
    {
        Guid PatientId { get; set; }
    }
}