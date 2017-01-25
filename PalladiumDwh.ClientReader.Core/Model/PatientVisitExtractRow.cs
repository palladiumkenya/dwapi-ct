using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FastMember;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Extracts;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class PatientVisitExtractRow : ExtractRow, IPatientVisitExtractRow
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int FacilityId { get; set; }
        public int SiteCode { get; set; }
        public string FacilityName { get; set; }
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Service { get; set; }
        public string VisitType { get; set; }
        public int WHOStage { get; set; }
        public string WABStage { get; set; }
        public string Pregnant { get; set; }
        public DateTime LMP { get; set; }
        public DateTime EDD { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BP { get; set; }
        public string OI { get; set; }
        public DateTime OIDate { get; set; }
        public string Adherence { get; set; }
        public string AdherenceCategory { get; set; }
        public DateTime SubstitutionFirstlineRegimenDate { get; set; }
        public string SubstitutionFirstlineRegimenReason { get; set; }
        public DateTime SubstitutionSecondlineRegimenDate { get; set; }
        public string SubstitutionSecondlineRegimenReason { get; set; }
        public DateTime SecondlineRegimenChangeDate { get; set; }
        public string SecondlineRegimenChangeReason { get; set; }
        public string FamilyPlanningMethod { get; set; }
        public string PwP { get; set; }
        public decimal GestationAge { get; set; }
        public DateTime NextAppointmentDate { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}
