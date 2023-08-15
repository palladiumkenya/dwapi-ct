using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class CovidExtract : Entity,ICovidExtract
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
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }

        public CovidExtract()
        {
            Created = DateTime.Now;
        }

        public CovidExtract(string facilityName, int? visitId, DateTime? covid19AssessmentDate, string receivedCovid19Vaccine, DateTime? dateGivenFirstDose, string firstDoseVaccineAdministered, DateTime? dateGivenSecondDose, string secondDoseVaccineAdministered, string vaccinationStatus, string vaccineVerification, string boosterGiven, string boosterDose, DateTime? boosterDoseDate, string everCovid19Positive, DateTime? covid19TestDate, string patientStatus, string admissionStatus, string admissionUnit, string missedAppointmentDueToCovid19, string covid19PositiveSinceLasVisit, DateTime? covid19TestDateSinceLastVisit, string patientStatusSinceLastVisit, string admissionStatusSinceLastVisit, DateTime? admissionStartDate, DateTime? admissionEndDate, string admissionUnitSinceLastVisit, string supplementalOxygenReceived, string patientVentilated, string tracingFinalOutcome, string causeOfDeath,string covid19TestResult,string sequence,string boosterDoseVerified,
            Guid patientId,string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified, string PatientUUID)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            Covid19AssessmentDate = covid19AssessmentDate;
            ReceivedCOVID19Vaccine = receivedCovid19Vaccine;
            DateGivenFirstDose = dateGivenFirstDose;
            FirstDoseVaccineAdministered = firstDoseVaccineAdministered;
            DateGivenSecondDose = dateGivenSecondDose;
            SecondDoseVaccineAdministered = secondDoseVaccineAdministered;
            VaccinationStatus = vaccinationStatus;
            VaccineVerification = vaccineVerification;
            BoosterGiven = boosterGiven;
            BoosterDose = boosterDose;
            BoosterDoseDate = boosterDoseDate;
            EverCOVID19Positive = everCovid19Positive;
            COVID19TestDate = covid19TestDate;
            PatientStatus = patientStatus;
            AdmissionStatus = admissionStatus;
            AdmissionUnit = admissionUnit;
            MissedAppointmentDueToCOVID19 = missedAppointmentDueToCovid19;
            COVID19PositiveSinceLasVisit = covid19PositiveSinceLasVisit;
            COVID19TestDateSinceLastVisit = covid19TestDateSinceLastVisit;
            PatientStatusSinceLastVisit = patientStatusSinceLastVisit;
            AdmissionStatusSinceLastVisit = admissionStatusSinceLastVisit;
            AdmissionStartDate = admissionStartDate;
            AdmissionEndDate = admissionEndDate;
            AdmissionUnitSinceLastVisit = admissionUnitSinceLastVisit;
            SupplementalOxygenReceived = supplementalOxygenReceived;
            PatientVentilated = patientVentilated;
            TracingFinalOutcome = tracingFinalOutcome;
            CauseOfDeath = causeOfDeath;
            COVID19TestResult = covid19TestResult;
            Sequence = sequence;
            BoosterDoseVerified = boosterDoseVerified;
            PatientUUID = PatientUUID;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
            this.StandardizeExtract();
        }
    }
}
