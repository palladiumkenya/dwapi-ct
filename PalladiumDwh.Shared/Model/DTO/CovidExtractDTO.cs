using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class CovidExtractDTO : ICovidExtractDTO
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? Covid19AssessmentDate { get; set; }
        public string ReceivedCOVID19Vaccine { get; set; }
        public DateTime? DateGivenFirstDose { get; set; }
        public string FirstDoseVaccineAdministered { get; set; }
        public DateTime? DateGivenSecondDose { get; set; }
        public string SecondDoseVaccineAdministered { get; set; }
        public string VaccinationStatus { get; set; }
        public string VaccineVerification { get; set; }
        public string BoosterGiven { get; set; }
        public string BoosterDose { get; set; }
        public DateTime? BoosterDoseDate { get; set; }
        public string EverCOVID19Positive { get; set; }
        public DateTime? COVID19TestDate { get; set; }
        public string PatientStatus { get; set; }
        public string AdmissionStatus { get; set; }
        public string AdmissionUnit { get; set; }
        public string MissedAppointmentDueToCOVID19 { get; set; }
        public string COVID19PositiveSinceLasVisit { get; set; }
        public DateTime? COVID19TestDateSinceLastVisit { get; set; }
        public string PatientStatusSinceLastVisit { get; set; }
        public string AdmissionStatusSinceLastVisit { get; set; }
        public DateTime? AdmissionStartDate { get; set; }
        public DateTime? AdmissionEndDate { get; set; }
        public string AdmissionUnitSinceLastVisit { get; set; }
        public string SupplementalOxygenReceived { get; set; }
        public string PatientVentilated { get; set; }
        public string TracingFinalOutcome { get; set; }
        public string CauseOfDeath { get; set; }
        public string COVID19TestResult { get; set; }
        public string Sequence { get; set; }
        public string BoosterDoseVerified { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public CovidExtractDTO()
        {
        }

        public CovidExtractDTO(CovidExtract CovidExtract)
        {
            FacilityName=CovidExtract.FacilityName;
            VisitID=CovidExtract.VisitID;
            Covid19AssessmentDate =CovidExtract.Covid19AssessmentDate;
            ReceivedCOVID19Vaccine =CovidExtract.ReceivedCOVID19Vaccine;
            DateGivenFirstDose =CovidExtract.DateGivenFirstDose;
            FirstDoseVaccineAdministered =CovidExtract.FirstDoseVaccineAdministered;
            DateGivenSecondDose =CovidExtract.DateGivenSecondDose;
            SecondDoseVaccineAdministered =CovidExtract.SecondDoseVaccineAdministered;
            VaccinationStatus =CovidExtract.VaccinationStatus;
            VaccineVerification =CovidExtract.VaccineVerification;
            BoosterGiven =CovidExtract.BoosterGiven;
            BoosterDose =CovidExtract.BoosterDose;
            BoosterDoseDate =CovidExtract.BoosterDoseDate;
            EverCOVID19Positive =CovidExtract.EverCOVID19Positive;
            COVID19TestDate =CovidExtract.COVID19TestDate;
            PatientStatus =CovidExtract.PatientStatus;
            AdmissionStatus =CovidExtract.AdmissionStatus;
            AdmissionUnit =CovidExtract.AdmissionUnit;
            MissedAppointmentDueToCOVID19 =CovidExtract.MissedAppointmentDueToCOVID19;
            COVID19PositiveSinceLasVisit =CovidExtract.COVID19PositiveSinceLasVisit;
            COVID19TestDateSinceLastVisit =CovidExtract.COVID19TestDateSinceLastVisit;
            PatientStatusSinceLastVisit =CovidExtract.PatientStatusSinceLastVisit;
            AdmissionStatusSinceLastVisit =CovidExtract.AdmissionStatusSinceLastVisit;
            AdmissionStartDate =CovidExtract.AdmissionStartDate;
            AdmissionEndDate =CovidExtract.AdmissionEndDate;
            AdmissionUnitSinceLastVisit =CovidExtract.AdmissionUnitSinceLastVisit;
            SupplementalOxygenReceived =CovidExtract.SupplementalOxygenReceived;
            PatientVentilated =CovidExtract.PatientVentilated;
            TracingFinalOutcome =CovidExtract.TracingFinalOutcome;
            CauseOfDeath =CovidExtract.CauseOfDeath;
            COVID19TestResult = CovidExtract.COVID19TestResult;
            Sequence =CovidExtract.Sequence;
            BoosterDoseVerified = CovidExtract.BoosterDoseVerified;

            PatientId=CovidExtract.PatientId;
            Emr =CovidExtract.Emr;
            Project =CovidExtract.Project;
        }

        public IEnumerable<CovidExtractDTO> GenerateCovidExtractDtOs(IEnumerable<CovidExtract> extracts)
        {
            var statusExtractDtos =new List<CovidExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new CovidExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public CovidExtract GenerateCovidExtract(Guid patientId)
        {
            PatientId = patientId;
            return new CovidExtract(
                FacilityName,
                VisitID,
                Covid19AssessmentDate ,
            ReceivedCOVID19Vaccine,
            DateGivenFirstDose ,
            FirstDoseVaccineAdministered ,
            DateGivenSecondDose ,
            SecondDoseVaccineAdministered ,
            VaccinationStatus ,
            VaccineVerification ,
            BoosterGiven ,
            BoosterDose ,
            BoosterDoseDate ,
            EverCOVID19Positive ,
            COVID19TestDate ,
            PatientStatus ,
            AdmissionStatus,
            AdmissionUnit ,
            MissedAppointmentDueToCOVID19 ,
            COVID19PositiveSinceLasVisit ,
            COVID19TestDateSinceLastVisit ,
            PatientStatusSinceLastVisit ,
            AdmissionStatusSinceLastVisit ,
            AdmissionStartDate,
            AdmissionEndDate,
            AdmissionUnitSinceLastVisit,
            SupplementalOxygenReceived ,
            PatientVentilated,
            TracingFinalOutcome ,
            CauseOfDeath ,
            COVID19TestResult,Sequence,BoosterDoseVerified,

                PatientId,Emr,Project
                );
        }


    }
}
