using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class DepressionScreeningExtract : Entity, IDepressionScreeningExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string PHQ9_1 { get; set; }
        public string PHQ9_2 { get; set; }
        public string PHQ9_3 { get; set; }
        public string PHQ9_4 { get; set; }
        public string PHQ9_5 { get; set; }
        public string PHQ9_6 { get; set; }
        public string PHQ9_7 { get; set; }
        public string PHQ9_8 { get; set; }
        public string PHQ9_9 { get; set; }
        public string PHQ_9_rating { get; set; }
        public int? DepressionAssesmentScore { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }


        public DepressionScreeningExtract()
        {
            Created=DateTime.Now;
        }

        public DepressionScreeningExtract(string facilityName, int? visitId, DateTime? visitDate, string phq91, string phq92, string phq93, string phq94, string phq95, string phq96, string phq97, string phq98, string phq99, string phq9Rating, int? depressionAssesmentScore,
            Guid patientId, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified, string patientUUID)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            PHQ9_1 = phq91;
            PHQ9_2 = phq92;
            PHQ9_3 = phq93;
            PHQ9_4 = phq94;
            PHQ9_5 = phq95;
            PHQ9_6 = phq96;
            PHQ9_7 = phq97;
            PHQ9_8 = phq98;
            PHQ9_9 = phq99;
            PHQ_9_rating = phq9Rating;
            DepressionAssesmentScore = depressionAssesmentScore;
            PatientUUID = patientUUID;
            
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }
    }
}
