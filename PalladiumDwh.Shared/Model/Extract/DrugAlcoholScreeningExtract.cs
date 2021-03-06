using System;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class DrugAlcoholScreeningExtract : Entity, IDrugAlcoholScreeningExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string DrinkingAlcohol { get; set; }
        public string Smoking { get; set; }
        public string DrugUse { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }

        public DrugAlcoholScreeningExtract()
        {
            Created = DateTime.Now;
        }

        public DrugAlcoholScreeningExtract(string facilityName, int? visitId, DateTime? visitDate, string drinkingAlcohol, string smoking, string drugUse,
            Guid patientId, string emr, string project)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            DrinkingAlcohol = drinkingAlcohol;
            Smoking = smoking;
            DrugUse = drugUse;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }
    }
}
