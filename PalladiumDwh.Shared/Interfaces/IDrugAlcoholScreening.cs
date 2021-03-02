using System;

namespace PalladiumDwh.Shared.Interfaces
{
  public   interface IDrugAlcoholScreening
    {
         string FacilityName { get; set; }
         int ? VisitID { get; set; }
         DateTime ? VisitDate { get; set; }
         string DrinkingAlcohol { get; set; }
         string Smoking { get; set; }
         string DrugUse { get; set; }
    }
}
