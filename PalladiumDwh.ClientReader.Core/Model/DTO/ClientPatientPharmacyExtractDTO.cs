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
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
        public int FacilityId { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }


        public ClientPatientPharmacyExtractDTO()
        {
        }

        public ClientPatientPharmacyExtractDTO(int? visitId, string drug, string provider, DateTime? dispenseDate, decimal? duration, DateTime? expectedReturn, string treatmentType, string regimenLine, string periodTaken, string prophylaxisType, int patientPid, string patientCccNumber, int facilityId, string emr, string project)
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
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
            FacilityId = facilityId;
            Emr = emr;
            Project = project;
        }

        public ClientPatientPharmacyExtractDTO(ClientPatientPharmacyExtract extract)
        {
            PatientPID = extract.PatientPK; //TODO PatientPID = extract.PatientPK;
            PatientCccNumber = extract.PatientID; //TODO PatientCccNumber = extract.PatientID;
            FacilityId = extract.SiteCode; //TODO FacilityId = extract.SiteCode
            VisitID = extract.VisitID;
            Drug = extract.Drug;
            Provider = extract.Provider;
            DispenseDate = extract.DispenseDate;
            Duration = extract.Duration;
            ExpectedReturn = extract.ExpectedReturn;
            TreatmentType = extract.TreatmentType;
            RegimenLine = extract.RegimenLine;
            PeriodTaken = extract.PeriodTaken;
            ProphylaxisType = extract.ProphylaxisType;

            Emr = extract.Emr;
            Project = extract.Project;

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

        public string RegimenChangedSwitched { get; set; }
        public string RegimenChangeSwitchReason { get; set; }
        public string StopRegimenReason { get; set; }
        public DateTime? StopRegimenDate { get; set; }
    }
}
