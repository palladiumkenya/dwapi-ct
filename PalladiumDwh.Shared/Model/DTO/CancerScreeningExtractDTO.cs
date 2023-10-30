using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class CancerScreeningExtractDTO : ICancerScreeningExtractDTO
    {
        public string Emr { get; set; }
        public string Project { get; set; }
        public string FacilityName { get; set; }
        public string VisitType { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string SmokesCigarette { get; set; }
        public int? NumberYearsSmoked { get; set; }
        public int? NumberCigarettesPerDay { get; set; }
        public string OtherFormTobacco { get; set; }
        public string TakesAlcohol { get; set; }
        public string HIVStatus { get; set; }
        public string FamilyHistoryOfCa { get; set; }
        public string PreviousCaTreatment { get; set; }
        public string SymptomsCa { get; set; }
        public string CancerType { get; set; }
        public string FecalOccultBloodTest { get; set; }
        public string TreatmentOccultBlood { get; set; }
        public string Colonoscopy { get; set; }
        public string TreatmentColonoscopy { get; set; }
        public string EUA { get; set; }
        public string TreatmentRetinoblastoma     { get; set; }
        public string RetinoblastomaGene  { get; set; }
        public string TreatmentEUA { get; set; }
        public string DRE { get; set; }
        public string TreatmentDRE { get; set; }
        public string PSA { get; set; }
        public string TreatmentPSA { get; set; }
        public string VisualExamination { get; set; }
        public string TreatmentVE { get; set; }
        public string Cytology { get; set; }
        public string TreatmentCytology { get; set; }
        public string Imaging { get; set; }
        public string TreatmentImaging { get; set; }
        public string Biopsy { get; set; }
        public string TreatmentBiopsy { get; set; }
        public string PostTreatmentComplicationCause { get; set; }
        public string OtherPostTreatmentComplication { get; set; }
        public string ReferralReason { get; set; }
        public string ScreeningMethod { get; set; }
        public string TreatmentToday { get; set; }
        public string ReferredOut { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ScreeningType { get; set; }
        public string HPVScreeningResult { get; set; }
        public string TreatmentHPV { get; set; }
        public string VIAScreeningResult { get; set; }
        public string VIAVILIScreeningResult { get; set; }
        public string VIATreatmentOptions { get; set; }
        public string PAPSmearScreeningResult { get; set; }
        public string TreatmentPapSmear { get; set; }
        public string ReferalOrdered { get; set; }
        public string Colposcopy { get; set; }
        public string TreatmentColposcopy { get; set; }
        public string BiopsyCINIIandAbove { get; set; }
        public string BiopsyCINIIandBelow { get; set; }
        public string BiopsyNotAvailable { get; set; }
        public string CBE { get; set; }
        public string TreatmentCBE { get; set; }
        public string Ultrasound { get; set; }
        public string TreatmentUltraSound { get; set; }
        public string IfTissueDiagnosis { get; set; }
        public DateTime? DateTissueDiagnosis { get; set; }
        public string ReasonNotDone { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string Referred { get; set; }
        public string ReasonForReferral { get; set; }
        public string RecordUUID { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }


        
        public CancerScreeningExtractDTO()
        {
        }

        public CancerScreeningExtractDTO(CancerScreeningExtract CancerScreeningExtract)
        {
            FacilityName=CancerScreeningExtract.FacilityName;
            VisitID=CancerScreeningExtract.VisitID;
            VisitDate=CancerScreeningExtract.VisitDate;
            
            VisitType = CancerScreeningExtract.VisitType;
            ScreeningMethod = CancerScreeningExtract.ScreeningMethod;
            TreatmentToday = CancerScreeningExtract.TreatmentToday;
            ReferredOut = CancerScreeningExtract.ReferredOut;
            NextAppointmentDate = CancerScreeningExtract.NextAppointmentDate;
            ScreeningType = CancerScreeningExtract.ScreeningType;
            PostTreatmentComplicationCause = CancerScreeningExtract.PostTreatmentComplicationCause;
            OtherPostTreatmentComplication = CancerScreeningExtract.OtherPostTreatmentComplication;
            ReferralReason = CancerScreeningExtract.ReferralReason;
            
            Emr=CancerScreeningExtract.Emr;
            Project=CancerScreeningExtract.Project;
            PatientId=CancerScreeningExtract.PatientId;
            Date_Created=CancerScreeningExtract.Date_Created;
            Date_Last_Modified=CancerScreeningExtract.Date_Last_Modified;
            RecordUUID=CancerScreeningExtract.RecordUUID;


        }

        public IEnumerable<CancerScreeningExtractDTO> GenerateCancerScreeningExtractDtOs(IEnumerable<CancerScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<CancerScreeningExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new CancerScreeningExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public CancerScreeningExtract GenerateCancerScreeningExtract(Guid patientId)
        {
            PatientId = patientId;
            return new CancerScreeningExtract(
                FacilityName, 
                VisitID,  
                VisitDate, 
                VisitType, 
                ScreeningMethod, 
                TreatmentToday, 
                ReferredOut,  
                NextAppointmentDate, 
                ScreeningType, 
                PostTreatmentComplicationCause, 
                OtherPostTreatmentComplication, 
                ReferralReason, 
                SmokesCigarette, 
                NumberYearsSmoked, 
                NumberCigarettesPerDay,
                OtherFormTobacco,
                TakesAlcohol,
                HIVStatus,
                FamilyHistoryOfCa,
                PreviousCaTreatment,
                SymptomsCa,
                CancerType,
                FecalOccultBloodTest,
                TreatmentOccultBlood,
                Colonoscopy,
                TreatmentColonoscopy,
                EUA,
                TreatmentRetinoblastoma, 
                RetinoblastomaGene ,
                TreatmentEUA,
                DRE,
                TreatmentDRE,
                PSA,
                TreatmentPSA,
                VisualExamination,
                TreatmentVE,
                Cytology,
                TreatmentCytology,
                Imaging,
                TreatmentImaging,
                Biopsy,
                TreatmentBiopsy,
                HPVScreeningResult,
                TreatmentHPV,
                VIAScreeningResult,
                VIAVILIScreeningResult,
                VIATreatmentOptions,
                PAPSmearScreeningResult,
                TreatmentPapSmear,
                ReferalOrdered,
                Colposcopy,
                TreatmentColposcopy,
                BiopsyCINIIandAbove,
                BiopsyCINIIandBelow,
                BiopsyNotAvailable,
                CBE,
                TreatmentCBE,
                Ultrasound,
                TreatmentUltraSound,
                IfTissueDiagnosis, 
                DateTissueDiagnosis,
                ReasonNotDone, 
                FollowUpDate,
                Referred,
                ReasonForReferral,
                PatientId, 
                Emr, 
                Project,  
                Date_Created, 
                Date_Last_Modified, 
                RecordUUID
                 
            );
        }

    }
}
