using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PalladiumDwh.Core.Model.DTO
{
    public class PatientProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientArtExtractDTO> ArtExtracts { get; set; }=new List<PatientArtExtractDTO>();
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; }=new List<PatientBaselinesExtractDTO>();
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; }=new List<PatientLaboratoryExtractDTO>();
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; }=new List<PatientPharmacyExtractDTO>();
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; }=new List<PatientStatusExtractDTO>();
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; }=new List<PatientVisitExtractDTO>();


        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        /*
        
        
        

        public DemographicDTO Demographic { get; set; }
        public List<PatientLabDTO> Labs { get; set; }
        public List<PatientVisitDTO> Visits { get; set; }
        public FacilityDTO Facility { get; set; }

        

      

        public List<PatientVisit> PatientVisitsInfo => _patientVisitsInfo;
        public List<PatientLab> PatientLabsInfo => _patientLabsInfo;

        public PatientProfile()
        {
        }

        public static PatientProfile Create(Facility facility,Patient patient)
        {
            var patientProfile=new PatientProfile();

            patientProfile.Facility = new FacilityDTO()
            {
                Code = facility.Code,
                Name = facility.Name
            };

            patientProfile.Demographic = new DemographicDTO()
            {
                DOB = patient.DOB,
                Name = patient.Name,
                Number = patient.Number,
                RegistrationDate = patient.RegistrationDate,
                Sex = patient.Sex
            };

            patientProfile.Visits = new List<PatientVisitDTO>();
            foreach (var v in patient.PatientVisits)
            {
                patientProfile.Visits.Add(new PatientVisitDTO()
                {
                    Height = v.Height,
                    Weight = v.Weight,
                    VisitDate = v.VisitDate,
                    WHOStage = v.WHOStage
                });

            }

            patientProfile.Labs = new List<PatientLabDTO>();
            foreach (var o in patient.PatientLabs)
            {
                patientProfile.Labs.Add(new PatientLabDTO()
                {
                    OrderedByDate = o.OrderedByDate,
                    ReportedByDate = o.ReportedByDate,
                    TestName = o.TestName,
                    TestResult = o.TestResult
                });
            }

            

            return patientProfile;
        }

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo =  Demographic.GeneratePatient(_facilityInfo.Id);
            _patientVisitsInfo = GetPatientVisits().ToList();
            _patientLabsInfo = GetPatientLabs().ToList();
            _recordGenerated = true;
        }

        public void UpdateMedicalRecords()
        {
            _patientVisitsInfo = GetPatientVisits().ToList();
            _patientLabsInfo = GetPatientLabs().ToList();
        }

        private IEnumerable<PatientLab> GetPatientLabs()
        {
             var labs=new List<PatientLab>();
            foreach (var lab in Labs)
            {
                labs.Add(lab.GeneratePatientLab(_patientInfo.Id));
            }
            return labs;
        }
        private IEnumerable<PatientVisit> GetPatientVisits()
        {
            var visits = new List<PatientVisit>();
            foreach (var visit in Visits)
            {
                visits.Add(visit.GeneratePatientVisit(_patientInfo.Id));
            }
            return visits;
        }

        public override string ToString()
        {
            return $"[{Demographic} | {Facility} | Visitis:{Visits.Count} | Labs:{Labs.Count}]";
        }
        public string GetMessageInfo()
        {
            return $"{Facility.Name}-{Demographic.Number}";
        }
        */
    }
}
