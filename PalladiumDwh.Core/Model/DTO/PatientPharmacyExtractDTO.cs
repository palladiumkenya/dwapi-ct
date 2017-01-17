using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model.DTO
{
    public class PatientPharmacyExtractDTO
    {
        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public string ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientPharmacyExtractDTO(PatientPharmacyExtract patientPharmacyExtract)
        {
            VisitID = patientPharmacyExtract.VisitID;
            Drug = patientPharmacyExtract.Drug;
            DispenseDate = patientPharmacyExtract.DispenseDate;
            Duration = patientPharmacyExtract.Duration;
            ExpectedReturn = patientPharmacyExtract.ExpectedReturn;
            TreatmentType = patientPharmacyExtract.TreatmentType;
            PeriodTaken = patientPharmacyExtract.PeriodTaken;
            ProphylaxisType = patientPharmacyExtract.ProphylaxisType;
            Emr = patientPharmacyExtract.Emr;
            Project = patientPharmacyExtract.Project;
            
            PatientId = patientPharmacyExtract.PatientId;
        }

        public PatientPharmacyExtractDTO()
        {
        }

        public IEnumerable<PatientPharmacyExtractDTO> GeneratePatientPharmacyExtractDtOs(IEnumerable<PatientPharmacyExtract> extracts)
        {
            var pharmacyExtractDtos = new List<PatientPharmacyExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                pharmacyExtractDtos.Add(new PatientPharmacyExtractDTO(e));
            }
            return pharmacyExtractDtos;
        }
        public PatientPharmacyExtract GeneratePatientPharmacyExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientPharmacyExtract(VisitID, Drug, DispenseDate, Duration, ExpectedReturn, TreatmentType,
                PeriodTaken, ProphylaxisType, Emr, Project,  PatientId);
        }
    }
}
