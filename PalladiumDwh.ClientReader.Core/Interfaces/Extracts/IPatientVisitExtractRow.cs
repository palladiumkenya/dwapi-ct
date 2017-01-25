using System;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientVisitExtractRow
    {
        int PatientPK { get; set; }
        string PatientID { get; set; }
        int FacilityId { get; set; }
        int SiteCode { get; set; }
        string FacilityName { get; set; }
        int VisitId { get; set; }
        DateTime VisitDate { get; set; }
        string Service { get; set; }
        string VisitType { get; set; }
        int WHOStage { get; set; }
        string WABStage { get; set; }
        string Pregnant { get; set; }
        DateTime LMP { get; set; }
        DateTime EDD { get; set; }
        decimal Height { get; set; }
        decimal Weight { get; set; }
        string BP { get; set; }
        string OI { get; set; }
        DateTime OIDate { get; set; }
        string Adherence { get; set; }
        string AdherenceCategory { get; set; }
        DateTime SubstitutionFirstlineRegimenDate { get; set; }
        string SubstitutionFirstlineRegimenReason { get; set; }
        DateTime SubstitutionSecondlineRegimenDate { get; set; }
        string SubstitutionSecondlineRegimenReason { get; set; }
        DateTime SecondlineRegimenChangeDate { get; set; }
        string SecondlineRegimenChangeReason { get; set; }
        string FamilyPlanningMethod { get; set; }
        string PwP { get; set; }
        decimal GestationAge { get; set; }
        DateTime NextAppointmentDate { get; set; }
    }
}