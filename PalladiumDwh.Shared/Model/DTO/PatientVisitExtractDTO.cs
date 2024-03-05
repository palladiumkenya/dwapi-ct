using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class PatientVisitExtractDTO : IPatientVisitExtractDTO
    {
        public int? VisitId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Service { get; set; }
        public string VisitType { get; set; }
        public int? WHOStage { get; set; }
        public string WABStage { get; set; }
        public string Pregnant { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string BP { get; set; }
        public string OI { get; set; }
        public DateTime? OIDate { get; set; }
        public DateTime? SubstitutionFirstlineRegimenDate { get; set; }
        public string SubstitutionFirstlineRegimenReason { get; set; }
        public DateTime? SubstitutionSecondlineRegimenDate { get; set; }
        public string SubstitutionSecondlineRegimenReason { get; set; }
        public DateTime? SecondlineRegimenChangeDate { get; set; }
        public string SecondlineRegimenChangeReason { get; set; }
        public string Adherence { get; set; }
        public string AdherenceCategory { get; set; }
        public string FamilyPlanningMethod { get; set; }
        public string PwP { get; set; }
        public decimal? GestationAge { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string StabilityAssessment { get; set; }
        public string DifferentiatedCare { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public string VisitBy { get; set; }
        public decimal? Temp { get; set; }
        public int? PulseRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public int? Muac { get; set; }
        public string NutritionalStatus { get; set; }
        public string EverHadMenses { get; set; }
        public string Breastfeeding { get; set; }
        public string Menopausal { get; set; }
        public string NoFPReason { get; set; }
        public string ProphylaxisUsed { get; set; }
        public string CTXAdherence { get; set; }
        public string CurrentRegimen { get; set; }
        public string HCWConcern { get; set; }
        public string TCAReason { get; set; }
        public string ClinicalNotes { get; set; }

        public string GeneralExamination { get; set; }
        public string SystemExamination { get; set; }
        public string Skin { get; set; }
        public string Eyes { get; set; }
        public string ENT { get; set; }
        public string Chest { get; set; }
        public string CVS { get; set; }
        public string Abdomen { get; set; }
        public string CNS { get; set; }
        public string Genitourinary { get; set; }
        public DateTime? RefillDate { get; set; }
        public string ZScore { get; set; }
        public int? ZScoreAbsolute { get; set; }
        public string PaedsDisclosure { get; set; }
        public string WHOStagingOI  { get; set; }

        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }



        public PatientVisitExtractDTO()
        {
        }

        public PatientVisitExtractDTO(int? visitId, DateTime? visitDate, string service, string visitType,
            int? whoStage, string wabStage, string pregnant, DateTime? lmp, DateTime? edd, decimal? height,
            decimal? weight, string bp, string oi, DateTime? oiDate, DateTime? substitutionFirstlineRegimenDate,
            string substitutionFirstlineRegimenReason, DateTime? substitutionSecondlineRegimenDate,
            string substitutionSecondlineRegimenReason, DateTime? secondlineRegimenChangeDate,
            string secondlineRegimenChangeReason, string adherence, string adherenceCategory,
            string familyPlanningMethod, string pwP, decimal? gestationAge, DateTime? nextAppointmentDate, string emr,
            string project, Guid patientId,
            string stabilityAssessment, string differentiatedCare, string populationType, string keyPopulationType, DateTime? refillDate, string zScore , int? zScoreAbsolute, string paedsDisclosure, DateTime? date_Created,DateTime? date_Last_Modified, string recordUUID, bool voided, string whoStagingOI 
        )
        {
            VisitId = visitId;
            VisitDate = visitDate;
            Service = service;
            VisitType = visitType;
            WHOStage = whoStage;
            WABStage = wabStage;
            Pregnant = pregnant;
            LMP = lmp;
            EDD = edd;
            Height = height;
            Weight = weight;
            BP = bp;
            OI = oi;
            OIDate = oiDate;
            SubstitutionFirstlineRegimenDate = substitutionFirstlineRegimenDate;
            SubstitutionFirstlineRegimenReason = substitutionFirstlineRegimenReason;
            SubstitutionSecondlineRegimenDate = substitutionSecondlineRegimenDate;
            SubstitutionSecondlineRegimenReason = substitutionSecondlineRegimenReason;
            SecondlineRegimenChangeDate = secondlineRegimenChangeDate;
            SecondlineRegimenChangeReason = secondlineRegimenChangeReason;
            Adherence = adherence;
            AdherenceCategory = adherenceCategory;
            FamilyPlanningMethod = familyPlanningMethod;
            PwP = pwP;
            GestationAge = gestationAge;
            NextAppointmentDate = nextAppointmentDate;
            Emr = emr;
            Project = project;
            PatientId = patientId;
            StabilityAssessment = stabilityAssessment;
            DifferentiatedCare = differentiatedCare;
            PopulationType = populationType;
            KeyPopulationType = keyPopulationType;
            RefillDate = refillDate;
            ZScore = zScore;
            ZScoreAbsolute = zScoreAbsolute;
            PaedsDisclosure = paedsDisclosure;
            WHOStagingOI = whoStagingOI;

            Date_Created=date_Created;
            Date_Last_Modified=date_Last_Modified;
            RecordUUID=recordUUID;
            Voided=voided;

        }

        public PatientVisitExtractDTO(PatientVisitExtract patientVisitExtract)
        {
            VisitId = patientVisitExtract.VisitId;
            VisitDate = patientVisitExtract.VisitDate;
            Service = patientVisitExtract.Service;
            VisitType = patientVisitExtract.VisitType;
            WHOStage = patientVisitExtract.WHOStage;
            WABStage = patientVisitExtract.WABStage;
            Pregnant = patientVisitExtract.Pregnant;
            LMP = patientVisitExtract.LMP;
            EDD = patientVisitExtract.EDD;
            Height = patientVisitExtract.Height;
            Weight = patientVisitExtract.Weight;
            BP = patientVisitExtract.BP;
            OI = patientVisitExtract.OI;
            OIDate = patientVisitExtract.OIDate;
            SubstitutionFirstlineRegimenDate = patientVisitExtract.SubstitutionFirstlineRegimenDate;
            SubstitutionFirstlineRegimenReason = patientVisitExtract.SubstitutionFirstlineRegimenReason;
            SubstitutionSecondlineRegimenDate = patientVisitExtract.SubstitutionSecondlineRegimenDate;
            SubstitutionSecondlineRegimenReason = patientVisitExtract.SubstitutionSecondlineRegimenReason;
            SecondlineRegimenChangeDate = patientVisitExtract.SecondlineRegimenChangeDate;
            SecondlineRegimenChangeReason = patientVisitExtract.SecondlineRegimenChangeReason;
            Adherence = patientVisitExtract.Adherence;
            AdherenceCategory = patientVisitExtract.AdherenceCategory;
            FamilyPlanningMethod = patientVisitExtract.FamilyPlanningMethod;
            PwP = patientVisitExtract.PwP;
            GestationAge = patientVisitExtract.GestationAge;
            NextAppointmentDate = patientVisitExtract.NextAppointmentDate;
            Emr = patientVisitExtract.Emr;
            Project = patientVisitExtract.Project;
            PatientId = patientVisitExtract.PatientId;
            StabilityAssessment = patientVisitExtract.StabilityAssessment;
            DifferentiatedCare = patientVisitExtract.DifferentiatedCare;
            PopulationType = patientVisitExtract.PopulationType;
            KeyPopulationType = patientVisitExtract.KeyPopulationType;
            RefillDate = patientVisitExtract.RefillDate;
            ZScore = patientVisitExtract.ZScore;
            ZScoreAbsolute = patientVisitExtract.ZScoreAbsolute;
            PaedsDisclosure = patientVisitExtract.PaedsDisclosure;
            Date_Created=patientVisitExtract.Date_Created;
            Date_Last_Modified=patientVisitExtract.Date_Last_Modified;
            RecordUUID=patientVisitExtract.RecordUUID;
            Voided=patientVisitExtract.Voided;
            WHOStagingOI = patientVisitExtract.WHOStagingOI;

        }



        public IEnumerable<PatientVisitExtractDTO> GeneratePatientVisitExtractDtOs(
            IEnumerable<PatientVisitExtract> extracts)
        {
            var visitExtractDtos = new List<PatientVisitExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                visitExtractDtos.Add(new PatientVisitExtractDTO(e));
            }

            return visitExtractDtos;
        }

        public PatientVisitExtract GeneratePatientVisitExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientVisitExtract(
                VisitId, VisitDate, Service, VisitType, WHOStage, WABStage, Pregnant, LMP, EDD, Height, Weight, BP, OI,
                OIDate,
                SubstitutionFirstlineRegimenDate, SubstitutionFirstlineRegimenReason, SubstitutionSecondlineRegimenDate,
                SubstitutionSecondlineRegimenReason, SecondlineRegimenChangeDate,
                SecondlineRegimenChangeReason, Adherence, AdherenceCategory, FamilyPlanningMethod, PwP, GestationAge,
                NextAppointmentDate, PatientId, Emr, Project, StabilityAssessment, DifferentiatedCare, PopulationType,
                KeyPopulationType,
                VisitBy, Temp, PulseRate, RespiratoryRate, OxygenSaturation, Muac, NutritionalStatus, EverHadMenses,
                Breastfeeding, Menopausal, NoFPReason, ProphylaxisUsed, CTXAdherence, CurrentRegimen, HCWConcern,
                TCAReason, ClinicalNotes,
                GeneralExamination, SystemExamination, Skin, Eyes, ENT, Chest, CVS, Abdomen, CNS, Genitourinary, RefillDate,ZScore,ZScoreAbsolute,PaedsDisclosure,Date_Created, Date_Last_Modified,
                RecordUUID,
                Voided, WHOStagingOI
            );
        }
    }
}
