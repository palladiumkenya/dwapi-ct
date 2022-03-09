using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Model
{
    public class StageDrugAlcoholScreeningExtract:StageExtract, IStageDrugAlcoholScreeningExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string DrinkingAlcohol { get; set; }
        public string Smoking { get; set; }
        public string DrugUse { get; set; }

        public  void Standardize(DrugAlcoholScreeningSourceBag sourceBag)
        {
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(DrugAlcoholScreeningSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
