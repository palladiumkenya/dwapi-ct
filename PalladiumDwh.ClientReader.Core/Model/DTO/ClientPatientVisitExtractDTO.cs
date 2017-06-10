using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.DTOs;

namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public class ClientPatientVisitExtractDTO : IClientPatientVisitExtractDTO
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
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
        public int FacilityId { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        

        public ClientPatientVisitExtractDTO()
        {
        }

        public ClientPatientVisitExtractDTO(int? visitId, DateTime? visitDate, string service, string visitType, int? whoStage, string wabStage, string pregnant, DateTime? lmp, DateTime? edd, decimal? height, decimal? weight, string bp, string oi, DateTime? oiDate, DateTime? substitutionFirstlineRegimenDate, string substitutionFirstlineRegimenReason, DateTime? substitutionSecondlineRegimenDate, string substitutionSecondlineRegimenReason, DateTime? secondlineRegimenChangeDate, string secondlineRegimenChangeReason, string adherence, string adherenceCategory, string familyPlanningMethod, string pwP, decimal? gestationAge, DateTime? nextAppointmentDate, int patientPid, string patientCccNumber, int facilityId, string emr, string project)
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
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
            FacilityId = facilityId;
            Emr = emr;
            Project = project;
        }

        public ClientPatientVisitExtractDTO(ClientPatientVisitExtract extract)
        {
            PatientPID = extract.PatientPK; //TODO PatientPID = extract.PatientPK;
            PatientCccNumber = extract.PatientID; //TODO PatientCccNumber = extract.PatientID;
            FacilityId = extract.SiteCode; //TODO FacilityId = extract.SiteCode
            VisitId = extract.VisitId;
            VisitDate = extract.VisitDate;
            Service = extract.Service;
            VisitType = extract.VisitType;
            WHOStage = extract.WHOStage;
            WABStage = extract.WABStage;
            Pregnant = extract.Pregnant;
            LMP = extract.LMP;
            EDD = extract.EDD;
            Height = extract.Height;
            Weight = extract.Weight;
            BP = extract.BP;
            OI = extract.OI;
            OIDate = extract.OIDate;
            SubstitutionFirstlineRegimenDate = extract.SubstitutionFirstlineRegimenDate;
            SubstitutionFirstlineRegimenReason = extract.SubstitutionFirstlineRegimenReason;
            SubstitutionSecondlineRegimenDate = extract.SubstitutionSecondlineRegimenDate;
            SubstitutionSecondlineRegimenReason = extract.SubstitutionSecondlineRegimenReason;
            SecondlineRegimenChangeDate = extract.SecondlineRegimenChangeDate;
            SecondlineRegimenChangeReason = extract.SecondlineRegimenChangeReason;
            Adherence = extract.Adherence;
            AdherenceCategory = extract.AdherenceCategory;
            FamilyPlanningMethod = extract.FamilyPlanningMethod;
            PwP = extract.PwP;
            GestationAge = extract.GestationAge;
            NextAppointmentDate = extract.NextAppointmentDate;
            
            Emr = extract.Emr;
            Project = extract.Project;

        }



        public IEnumerable<ClientPatientVisitExtractDTO> GeneratePatientVisitExtractDtOs(IEnumerable<ClientPatientVisitExtract> extracts)
        {
            var visitExtractDtos = new List<ClientPatientVisitExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                visitExtractDtos.Add(new ClientPatientVisitExtractDTO(e));
            }
            return visitExtractDtos;
        }
    }
}