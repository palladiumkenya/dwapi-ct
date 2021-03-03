using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class EnhancedAdherenceCounsellingExtractDTO : IEnhancedAdherenceCounsellingExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public EnhancedAdherenceCounsellingExtractDTO()
        {
        }

        public EnhancedAdherenceCounsellingExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public EnhancedAdherenceCounsellingExtractDTO(EnhancedAdherenceCounsellingExtract EnhancedAdherenceCounsellingExtract)
        {
            // ExitDescription = EnhancedAdherenceCounsellingExtract.ExitDescription;
            // ExitDate = EnhancedAdherenceCounsellingExtract.ExitDate;
            // ExitReason = EnhancedAdherenceCounsellingExtract.ExitReason;
            Emr = EnhancedAdherenceCounsellingExtract.Emr;
            Project = EnhancedAdherenceCounsellingExtract.Project;
            PatientId = EnhancedAdherenceCounsellingExtract.PatientId;
        }



        public IEnumerable<EnhancedAdherenceCounsellingExtractDTO> GenerateEnhancedAdherenceCounsellingExtractDtOs(IEnumerable<EnhancedAdherenceCounsellingExtract> extracts)
        {
            var statusExtractDtos = new List<EnhancedAdherenceCounsellingExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new EnhancedAdherenceCounsellingExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public EnhancedAdherenceCounsellingExtract GenerateEnhancedAdherenceCounsellingExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new EnhancedAdherenceCounsellingExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new EnhancedAdherenceCounsellingExtract();
        }


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
    }
}
