using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageBaselineExtract:StageExtract, IStageBaselineExtract
    {
        public int? bCD4 { get; set; }
        public DateTime? bCD4Date { get; set; }
        public int? bWAB { get; set; }
        public DateTime? bWABDate { get; set; }
        public int? bWHO { get; set; }
        public DateTime? bWHODate { get; set; }
        public int? eWAB { get; set; }        public DateTime? eWABDate { get; set; }
        public int? eCD4 { get; set; }
        public DateTime? eCD4Date { get; set; }
        public int? eWHO { get; set; }
        public DateTime? eWHODate { get; set; }
        public int? lastWHO { get; set; }
        public DateTime? lastWHODate { get; set; }
        public int? lastCD4 { get; set; }
        public DateTime? lastCD4Date { get; set; }
        public int? lastWAB { get; set; }
        public DateTime? lastWABDate { get; set; }
        public int? m12CD4 { get; set; }
        public DateTime? m12CD4Date { get; set; }
        public int? m6CD4 { get; set; }
        public DateTime? m6CD4Date { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }


        public  void Standardize(BaselineSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(BaselineSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
