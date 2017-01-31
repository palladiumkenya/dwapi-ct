using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.DTOs;

namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public class ClientPatientPharmacyExtractDTO : IClientPatientPharmacyExtractDTO
    {
        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public ClientPatientPharmacyExtractDTO()
        {
        }

        public ClientPatientPharmacyExtractDTO(int? visitId, string drug, string provider, DateTime? dispenseDate, decimal? duration, DateTime? expectedReturn, string treatmentType, string regimenLine, string periodTaken, string prophylaxisType, string emr, string project, Guid patientId)
        {
            VisitID = visitId;
            Drug = drug;
            Provider = provider;
            DispenseDate = dispenseDate;
            Duration = duration;
            ExpectedReturn = expectedReturn;
            TreatmentType = treatmentType;
            RegimenLine = regimenLine;
            PeriodTaken = periodTaken;
            ProphylaxisType = prophylaxisType;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public ClientPatientPharmacyExtractDTO(ClientPatientPharmacyExtract patientPharmacyExtract)
        {
            VisitID = patientPharmacyExtract.VisitID;
            Drug = patientPharmacyExtract.Drug;
            Provider = patientPharmacyExtract.Provider;
            DispenseDate = patientPharmacyExtract.DispenseDate;
            Duration = patientPharmacyExtract.Duration;
            ExpectedReturn = patientPharmacyExtract.ExpectedReturn;
            TreatmentType = patientPharmacyExtract.TreatmentType;
            PeriodTaken = patientPharmacyExtract.PeriodTaken;
            RegimenLine = patientPharmacyExtract.RegimenLine;
            PeriodTaken = patientPharmacyExtract.PeriodTaken;
            ProphylaxisType = patientPharmacyExtract.ProphylaxisType;
            Emr = patientPharmacyExtract.Emr;
            Project = patientPharmacyExtract.Project;
            //PatientId = patientPharmacyExtract.PatientId;
        }

        public IEnumerable<ClientPatientPharmacyExtractDTO> GeneratePatientPharmacyExtractDtOs(IEnumerable<ClientPatientPharmacyExtract> extracts)
        {
            var pharmacyExtractDtos = new List<ClientPatientPharmacyExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                pharmacyExtractDtos.Add(new ClientPatientPharmacyExtractDTO(e));
            }
            return pharmacyExtractDtos;
        }
        public ClientPatientPharmacyExtract GeneratePatientPharmacyExtract(Guid patientId)
        {
            PatientId = patientId;
            return new ClientPatientPharmacyExtract();
//                VisitID, Drug,Provider, DispenseDate, Duration, ExpectedReturn, TreatmentType,
//                RegimenLine,
//                PeriodTaken, ProphylaxisType, PatientId,Emr, Project);
        }
    }
}
