using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
  public   interface IDrugAlcoholScreeningExtract : IExtract,IDrugAlcoholScreening
    {
         Guid PatientId { get; set; }
    }
}
