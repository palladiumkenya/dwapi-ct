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

        public PatientVisitExtractDTO()
        {
        }

        public PatientVisitExtractDTO(int? visitId, DateTime? visitDate, string service, string visitType, int? whoStage, string wabStage, string pregnant, DateTime? lmp, DateTime? edd, decimal? height, decimal? weight, string bp, string oi, DateTime? oiDate, DateTime? substitutionFirstlineRegimenDate, string substitutionFirstlineRegimenReason, DateTime? substitutionSecondlineRegimenDate, string substitutionSecondlineRegimenReason, DateTime? secondlineRegimenChangeDate, string secondlineRegimenChangeReason, string adherence, string adherenceCategory, string familyPlanningMethod, string pwP, decimal? gestationAge, DateTime? nextAppointmentDate, string emr, string project, Guid patientId)
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
        }

        public PatientVisitExtractDTO(PatientVisitExtract patientVisitExtract)
        {
            VisitId = patientVisitExtract.VisitId;
            VisitDate = patientVisitExtract.VisitDate;
            Service = patientVisitExtract.Service;
            VisitType =patientVisitExtract.VisitType;
            WHOStage =patientVisitExtract.WHOStage;
            WABStage =patientVisitExtract.WABStage;
            Pregnant =patientVisitExtract.Pregnant;
            LMP =patientVisitExtract.LMP;
            EDD =patientVisitExtract.EDD;
            Height =patientVisitExtract.Height;
            Weight =patientVisitExtract.Weight;
            BP =patientVisitExtract.BP;
            OI =patientVisitExtract.OI;
            OIDate =patientVisitExtract.OIDate;
            SubstitutionFirstlineRegimenDate = patientVisitExtract.SubstitutionFirstlineRegimenDate;
            SubstitutionFirstlineRegimenReason =patientVisitExtract.SubstitutionFirstlineRegimenReason;
            SubstitutionSecondlineRegimenDate =patientVisitExtract.SubstitutionSecondlineRegimenDate;
            SubstitutionSecondlineRegimenReason =patientVisitExtract.SubstitutionSecondlineRegimenReason;
            SecondlineRegimenChangeDate =patientVisitExtract.SecondlineRegimenChangeDate;
            SecondlineRegimenChangeReason =patientVisitExtract.SecondlineRegimenChangeReason;
            Adherence =patientVisitExtract.Adherence;
            AdherenceCategory =patientVisitExtract.AdherenceCategory;
            FamilyPlanningMethod = patientVisitExtract.FamilyPlanningMethod;
            PwP =patientVisitExtract.PwP;
            GestationAge = patientVisitExtract.GestationAge;
            NextAppointmentDate = patientVisitExtract.NextAppointmentDate;
            Emr = patientVisitExtract.Emr;
            Project = patientVisitExtract.Project;
            PatientId = patientVisitExtract.PatientId;
        }

        

        public IEnumerable<PatientVisitExtractDTO> GeneratePatientVisitExtractDtOs(IEnumerable<PatientVisitExtract> extracts)
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
                VisitId, VisitDate, Service,VisitType,WHOStage,WABStage, Pregnant, LMP, EDD, Height, Weight, BP, OI, OIDate,
                SubstitutionFirstlineRegimenDate, SubstitutionFirstlineRegimenReason, SubstitutionSecondlineRegimenDate,
                SubstitutionSecondlineRegimenReason, SecondlineRegimenChangeDate,
                SecondlineRegimenChangeReason, Adherence, AdherenceCategory, FamilyPlanningMethod, PwP, GestationAge,
                NextAppointmentDate, PatientId,Emr, Project);
        }
    }
}
