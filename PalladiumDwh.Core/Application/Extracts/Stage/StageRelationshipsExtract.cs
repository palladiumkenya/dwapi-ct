using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageRelationshipsExtract:StageExtract, IStageRelationshipsExtract
    {
        public string FacilityName { get; set; }

        public string RelationshipToPatient { get; set; }
        public int PersonAPatientPk { get; set; }
        public int PersonBPatientPk { get; set; }
        public string PatientRelationshipToOther { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }

        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }


        public  void Standardize(RelationshipsSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(RelationshipsSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
