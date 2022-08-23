using System;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class PatientVisitExtract:Entity, IPatientVisitExtract
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
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }


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

        public PatientVisitExtract()
        {
            Created = DateTime.Now;
        }

        public PatientVisitExtract(int? visitId, DateTime? visitDate, string service, string visitType, int? whoStage, string wabStage, string pregnant, DateTime? lmp, DateTime? edd, decimal? height, decimal? weight, string bp, string oi, DateTime? oiDate, DateTime? substitutionFirstlineRegimenDate, string substitutionFirstlineRegimenReason, DateTime? substitutionSecondlineRegimenDate, string substitutionSecondlineRegimenReason, DateTime? secondlineRegimenChangeDate, string secondlineRegimenChangeReason, string adherence, string adherenceCategory, string familyPlanningMethod, string pwP, decimal? gestationAge, DateTime? nextAppointmentDate, Guid patientId, string emr, string project,
            string stabilityAssessment, string differentiatedCare, string populationType, string keyPopulationType,
            string visitBy, decimal? temp, int? pulseRate, int? respiratoryRate, decimal? oxygenSaturation, int? muac, string nutritionalStatus, string everHadMenses, string breastfeeding, string menopausal, string noFpReason, string prophylaxisUsed, string ctxAdherence, string currentRegimen, string hcwConcern, string tcaReason, string clinicalNotes,
            string generalExamination,	string systemExamination,	string skin,	string eyes,	string ent,	string chest,	string cvs,	string abdomen,	string cns,	string genitourinary, DateTime? refillDate)
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
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            StabilityAssessment = stabilityAssessment;
            DifferentiatedCare = differentiatedCare;
            PopulationType = populationType;
            KeyPopulationType = keyPopulationType;

            VisitBy = visitBy;
            Temp = temp;
            PulseRate = pulseRate;
            RespiratoryRate = respiratoryRate;
            OxygenSaturation = oxygenSaturation;
            Muac = muac;
            NutritionalStatus = nutritionalStatus;
            EverHadMenses = everHadMenses;
            Breastfeeding = breastfeeding;
            Menopausal = menopausal;
            NoFPReason = noFpReason;
            ProphylaxisUsed = prophylaxisUsed;
            CTXAdherence = ctxAdherence;
            CurrentRegimen = currentRegimen;
            HCWConcern = hcwConcern;
            TCAReason = tcaReason;
            ClinicalNotes = clinicalNotes;

            GeneralExamination=generalExamination;
            SystemExamination=systemExamination;
            Skin=skin;
            Eyes=eyes;
            ENT=ent;
            Chest=chest;
            CVS=cvs;
            Abdomen=abdomen;
            CNS=cns;
            Genitourinary=genitourinary;
            RefillDate = refillDate;
        }
    }
}
