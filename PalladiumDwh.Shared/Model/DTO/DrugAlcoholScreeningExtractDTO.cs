using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class DrugAlcoholScreeningExtractDTO : IDrugAlcoholScreeningExtractDTO
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string DrinkingAlcohol { get; set; }
        public string Smoking { get; set; }
        public string DrugUse { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DrugAlcoholScreeningExtractDTO()
        {
        }

        public DrugAlcoholScreeningExtractDTO(DrugAlcoholScreeningExtract DrugAlcoholScreeningExtract)
        {
            FacilityName=DrugAlcoholScreeningExtract.FacilityName;
            VisitID=DrugAlcoholScreeningExtract.VisitID;
            VisitDate=DrugAlcoholScreeningExtract.VisitDate;
            DrinkingAlcohol=DrugAlcoholScreeningExtract.DrinkingAlcohol;
            Smoking=DrugAlcoholScreeningExtract.Smoking;
            DrugUse=DrugAlcoholScreeningExtract.DrugUse;

            Emr = DrugAlcoholScreeningExtract.Emr;
            Project = DrugAlcoholScreeningExtract.Project;
            PatientId = DrugAlcoholScreeningExtract.PatientId;
            Date_Created=DrugAlcoholScreeningExtract.Date_Created;
            Date_Last_Modified=DrugAlcoholScreeningExtract.Date_Last_Modified;
        }

        public IEnumerable<DrugAlcoholScreeningExtractDTO> GenerateDrugAlcoholScreeningExtractDtOs(IEnumerable<DrugAlcoholScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<DrugAlcoholScreeningExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new DrugAlcoholScreeningExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public DrugAlcoholScreeningExtract GenerateDrugAlcoholScreeningExtract(Guid patientId)
        {
            PatientId = patientId;
            return new DrugAlcoholScreeningExtract(
                FacilityName,
                VisitID,
                VisitDate,
                DrinkingAlcohol,
                Smoking,
                DrugUse,
                PatientId,
                Emr,Project,
                Date_Created,
                Date_Last_Modified
                );
        }


    }
}
