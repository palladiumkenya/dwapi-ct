using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageContactListingExtract:StageExtract, IStageContactListingExtract
    {
        public string FacilityName { get; set; }
        public int? PartnerPersonID { get; set; }
        public string ContactAge { get; set; }
        public string ContactSex { get; set; }
        public string ContactMaritalStatus { get; set; }
        public string RelationshipWithPatient { get; set; }
        public string ScreenedForIpv { get; set; }
        public string IpvScreening { get; set; }
        public string IPVScreeningOutcome { get; set; }
        public string CurrentlyLivingWithIndexClient { get; set; }
        public string KnowledgeOfHivStatus { get; set; }
        public string PnsApproach { get; set; }
        public int? ContactPatientPK { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }


        public  void Standardize(ContactListingSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(ContactListingSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
