using System;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class EnhancedAdherenceCounsellingExtract : Entity, IEnhancedAdherenceCounsellingExtract
    {

        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? SessionNumber { get; set; }
        public DateTime? DateOfFirstSession { get; set; }
        public int? PillCountAdherence { get; set; }
        public string MMAS4_1 { get; set; }
        public string MMAS4_2 { get; set; }
        public string MMAS4_3 { get; set; }
        public string MMAS4_4 { get; set; }
        public string MMSA8_1 { get; set; }
        public string MMSA8_2 { get; set; }
        public string MMSA8_3 { get; set; }
        public string MMSA8_4 { get; set; }
        public string MMSAScore { get; set; }
        public string EACRecievedVL { get; set; }
        public string EACVL { get; set; }
        public string EACVLConcerns { get; set; }
        public string EACVLThoughts { get; set; }
        public string EACWayForward { get; set; }
        public string EACCognitiveBarrier { get; set; }
        public string EACBehaviouralBarrier_1 { get; set; }
        public string EACBehaviouralBarrier_2 { get; set; }
        public string EACBehaviouralBarrier_3 { get; set; }
        public string EACBehaviouralBarrier_4 { get; set; }
        public string EACBehaviouralBarrier_5 { get; set; }
        public string EACEmotionalBarriers_1 { get; set; }
        public string EACEmotionalBarriers_2 { get; set; }
        public string EACEconBarrier_1 { get; set; }
        public string EACEconBarrier_2 { get; set; }
        public string EACEconBarrier_3 { get; set; }
        public string EACEconBarrier_4 { get; set; }
        public string EACEconBarrier_5 { get; set; }
        public string EACEconBarrier_6 { get; set; }
        public string EACEconBarrier_7 { get; set; }
        public string EACEconBarrier_8 { get; set; }
        public string EACReviewImprovement { get; set; }
        public string EACReviewMissedDoses { get; set; }
        public string EACReviewStrategy { get; set; }
        public string EACReferral { get; set; }
        public string EACReferralApp { get; set; }
        public string EACReferralExperience { get; set; }
        public string EACHomevisit { get; set; }
        public string EACAdherencePlan { get; set; }
        public DateTime? EACFollowupDate { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }

        public EnhancedAdherenceCounsellingExtract()
        {
            Created = DateTime.Now;
        }

        public EnhancedAdherenceCounsellingExtract(string facilityName, int? visitId, DateTime? visitDate, int? sessionNumber, DateTime? dateOfFirstSession, int? pillCountAdherence, string mmas41, string mmas42, string mmas43, string mmas44, string mmsa81, string mmsa82, string mmsa83, string mmsa84, string mmsaScore, string eacRecievedVl, string eacvl, string eacvlConcerns, string eacvlThoughts, string eacWayForward, string eacCognitiveBarrier, string eacBehaviouralBarrier1, string eacBehaviouralBarrier2, string eacBehaviouralBarrier3, string eacBehaviouralBarrier4, string eacBehaviouralBarrier5, string eacEmotionalBarriers1, string eacEmotionalBarriers2, string eacEconBarrier1, string eacEconBarrier2, string eacEconBarrier3, string eacEconBarrier4, string eacEconBarrier5, string eacEconBarrier6, string eacEconBarrier7, string eacEconBarrier8, string eacReviewImprovement, string eacReviewMissedDoses, string eacReviewStrategy, string eacReferral, string eacReferralApp, string eacReferralExperience, string eacHomevisit, string eacAdherencePlan, DateTime? eacFollowupDate,
            Guid patientId, string emr, string project)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            SessionNumber = sessionNumber;
            DateOfFirstSession = dateOfFirstSession;
            PillCountAdherence = pillCountAdherence;
            MMAS4_1 = mmas41;
            MMAS4_2 = mmas42;
            MMAS4_3 = mmas43;
            MMAS4_4 = mmas44;
            MMSA8_1 = mmsa81;
            MMSA8_2 = mmsa82;
            MMSA8_3 = mmsa83;
            MMSA8_4 = mmsa84;
            MMSAScore = mmsaScore;
            EACRecievedVL = eacRecievedVl;
            EACVL = eacvl;
            EACVLConcerns = eacvlConcerns;
            EACVLThoughts = eacvlThoughts;
            EACWayForward = eacWayForward;
            EACCognitiveBarrier = eacCognitiveBarrier;
            EACBehaviouralBarrier_1 = eacBehaviouralBarrier1;
            EACBehaviouralBarrier_2 = eacBehaviouralBarrier2;
            EACBehaviouralBarrier_3 = eacBehaviouralBarrier3;
            EACBehaviouralBarrier_4 = eacBehaviouralBarrier4;
            EACBehaviouralBarrier_5 = eacBehaviouralBarrier5;
            EACEmotionalBarriers_1 = eacEmotionalBarriers1;
            EACEmotionalBarriers_2 = eacEmotionalBarriers2;
            EACEconBarrier_1 = eacEconBarrier1;
            EACEconBarrier_2 = eacEconBarrier2;
            EACEconBarrier_3 = eacEconBarrier3;
            EACEconBarrier_4 = eacEconBarrier4;
            EACEconBarrier_5 = eacEconBarrier5;
            EACEconBarrier_6 = eacEconBarrier6;
            EACEconBarrier_7 = eacEconBarrier7;
            EACEconBarrier_8 = eacEconBarrier8;
            EACReviewImprovement = eacReviewImprovement;
            EACReviewMissedDoses = eacReviewMissedDoses;
            EACReviewStrategy = eacReviewStrategy;
            EACReferral = eacReferral;
            EACReferralApp = eacReferralApp;
            EACReferralExperience = eacReferralExperience;
            EACHomevisit = eacHomevisit;
            EACAdherencePlan = eacAdherencePlan;
            EACFollowupDate = eacFollowupDate;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }
    }
}
