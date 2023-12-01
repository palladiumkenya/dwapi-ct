using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageArtFastTrackExtract:StageExtract, IStageArtFastTrackExtract
    {
        public string FacilityName { get; set; }

        public string ARTRefillModel  { get; set; }
        public DateTime?  VisitDate  { get; set; }
        public string CTXDispensed  { get; set; }
        public string DapsoneDispensed  { get; set; }
        public string CondomsDistributed  { get; set; }
        public string OralContraceptivesDispensed  { get; set; }
        public string MissedDoses  { get; set; }
        public string Fatigue  { get; set; }
        public string Cough  { get; set; }
        public string Fever  { get; set; }
        public string Rash  { get; set; }
        public string NauseaOrVomiting { get; set; }
        public string GenitalSoreOrDischarge  { get; set; }
        public string Diarrhea  { get; set; }
        public string OtherSymptoms  { get; set; }
        public string PregnancyStatus  { get; set; }
        public string FPStatus  { get; set; }
        public string FPMethod  { get; set; }
        public string ReasonNotOnFP  { get; set; }
        public string ReferredToClinic  { get; set; }
        public DateTime?  ReturnVisitDate  { get; set; }
        
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }

        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }


        public  void Standardize(ArtFastTrackSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(ArtFastTrackSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
