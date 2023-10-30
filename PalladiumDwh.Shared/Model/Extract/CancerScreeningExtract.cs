using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class CancerScreeningExtract : Entity, ICancerScreeningExtract
    {
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
        public DateTime? Created { get; set; }


        public CancerScreeningExtract()
        {
            Created = DateTime.Now;
        }

        public CancerScreeningExtract(string facilityName, int? visitID, DateTime? visitDate, string visitType,
                string screeningMethod, string treatmentToday, string referredOut, DateTime? nextAppointmentDate,
                string screeningType, string postTreatmentComplicationCause, string otherPostTreatmentComplication,
                string referralReason,
                string smokesCigarette, int? numberYearsSmoked, int? numberCigarettesPerDay, string otherFormTobacco,
                string takesAlcohol, string hivStatus, string familyHistoryOfCa, string previousCaTreatment,
                string symptomsCa, string cancerType, string fecalOccultBloodTest, string treatmentOccultBlood,
                string colonoscopy, string treatmentColonoscopy, string eua, string treatmentRetinoblastoma,
                string retinoblastomaGene, string treatmentEUA, string dre, string treatmentDRE, string psa,
                string treatmentPSA, string visualExamination, string treatmentVE, string cytology,
                string treatmentCytology, string imaging, string treatmentImaging, string biopsy,
                string treatmentBiopsy,
                string hpvScreeningResult, string treatmentHPV, string viaScreeningResult,
                string viaVILIScreeningResult, string viaTreatmentOptions, string papSmearScreeningResult,
                string treatmentPapSmear, string referalOrdered, string colposcopy, string treatmentColposcopy,
                string biopsyCINIIandAbove, string biopsyCINIIandBelow, string biopsyNotAvailable, string cbe,
                string treatmentCBE, string ultrasound, string treatmentUltraSound, string ifTissueDiagnosis,
                DateTime? dateTissueDiagnosis, string reasonNotDone, DateTime? followUpDate, string referred,
                string reasonForReferral,
                Guid patientId, string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified,
                string recordUUID)
        // public CancerScreeningExtract(string facilityName, int? visitId, DateTime? visitDate, string visitType, string smokesCigarette, int? numberYearsSmoked, int? numberCigarettesPerDay, string nextAppointmentDate, string screeningType, string postTreatmentComplicationCause, string otherPostTreatmentComplication, string previousCaTreatment, string symptomsCa, string cancerType, string fecalOccultBloodTest, string treatmentOccultBlood, string takesAlcohol, 
        //     string treatmentColonoscopy, string familyHistoryOfCa, string treatmentRetinoblastoma, string retinoblastomaGene, string treatmentEUA, string dre, string treatmentDRE, string colonoscopy, string treatmentPSA, string visualExamination, string treatmentVE, string cytology, string treatmentCytology, string imaging, string treatmentImaging, string biopsy, string treatmentBiopsy, string hpvScreeningResult, string treatmentHPV, string viaScreeningResult, 
        //     string OtherFormTobacco, string viaVILIScreeningResult, string viaTreatmentOptions, string papSmearScreeningResult, string treatmentPapSmear, string referalOrdered, string colposcopy, string treatmentColposcopy, string biopsyCINIIandAbove, string viaVILIScreeningResult, string biopsyNotAvailable, string cbe, string treatmentCBE, string ultrasound, string treatmentUltraSound, string ifTissueDiagnosis, DateTime? dateTissueDiagnosis, string biopsyCINIIandBelow, DateTime? followUpDate, 
        //     string referred, string reasonForReferral, Guid patientId, string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified, string recordUuid, bool? voided)
        {
            FacilityName = facilityName;
            VisitID = visitID;
            VisitDate = visitDate;

            VisitType = visitType;
            ScreeningMethod = screeningMethod;
            TreatmentToday = treatmentToday;
            ReferredOut = referredOut;
            NextAppointmentDate = nextAppointmentDate;
            ScreeningType = screeningType;
            PostTreatmentComplicationCause = postTreatmentComplicationCause;
            OtherPostTreatmentComplication = otherPostTreatmentComplication;
            ReferralReason = referralReason;
            RecordUUID = recordUUID;
            
            SmokesCigarette = smokesCigarette;
            NumberYearsSmoked = numberYearsSmoked;
            NumberCigarettesPerDay = numberCigarettesPerDay;
            OtherFormTobacco = otherFormTobacco;
            TakesAlcohol = takesAlcohol;
            HIVStatus = hivStatus;
            FamilyHistoryOfCa = familyHistoryOfCa;
            PreviousCaTreatment = previousCaTreatment;
            SymptomsCa = symptomsCa;
            CancerType = cancerType;
            FecalOccultBloodTest = fecalOccultBloodTest;
            TreatmentOccultBlood = treatmentOccultBlood;
            Colonoscopy = colonoscopy;
            TreatmentColonoscopy = treatmentColonoscopy;
            EUA = eua;
            TreatmentRetinoblastoma     = treatmentRetinoblastoma    ;
            RetinoblastomaGene  = retinoblastomaGene ;
            TreatmentEUA = treatmentEUA;
            DRE = dre;
            TreatmentDRE = treatmentDRE;
            PSA = psa;
            TreatmentPSA = treatmentPSA;
            VisualExamination = visualExamination;
            TreatmentVE = treatmentVE;
            Cytology = cytology;
            TreatmentCytology = treatmentCytology;
            Imaging = imaging;
            TreatmentImaging = treatmentImaging;
            Biopsy = biopsy;
            TreatmentBiopsy = treatmentBiopsy;

            HPVScreeningResult = hpvScreeningResult;
            TreatmentHPV = treatmentHPV;
            VIAScreeningResult = viaScreeningResult;
            VIAVILIScreeningResult = viaVILIScreeningResult;
            VIATreatmentOptions = viaTreatmentOptions;
            PAPSmearScreeningResult = papSmearScreeningResult;
            TreatmentPapSmear = treatmentPapSmear;
            ReferalOrdered = referalOrdered;
            Colposcopy = colposcopy;
            TreatmentColposcopy = treatmentColposcopy;
            BiopsyCINIIandAbove = biopsyCINIIandAbove;
            BiopsyCINIIandBelow = biopsyCINIIandBelow;
            BiopsyNotAvailable = biopsyNotAvailable;
            CBE = cbe;
            TreatmentCBE = treatmentCBE;
            Ultrasound = ultrasound;
            TreatmentUltraSound = treatmentUltraSound;
            IfTissueDiagnosis = ifTissueDiagnosis;
            DateTissueDiagnosis = dateTissueDiagnosis;
            ReasonNotDone = reasonNotDone;
            FollowUpDate = followUpDate;
            Referred = referred;
            ReasonForReferral = reasonForReferral;
            
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }

       
        public Guid PatientId { get; set; }
    }
}
