using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public   interface IDrugAlcoholScreeningExtractDTO : IExtractDTO,IDrugAlcoholScreening
    {
        Guid PatientId { get; set; }
    }
}