using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class DrugAlcoholScreeningExtractDTO : IDrugAlcoholScreeningExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DrugAlcoholScreeningExtractDTO()
        {
        }

        public DrugAlcoholScreeningExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public DrugAlcoholScreeningExtractDTO(DrugAlcoholScreeningExtract DrugAlcoholScreeningExtract)
        {
            // ExitDescription = DrugAlcoholScreeningExtract.ExitDescription;
            // ExitDate = DrugAlcoholScreeningExtract.ExitDate;
            // ExitReason = DrugAlcoholScreeningExtract.ExitReason;
            Emr = DrugAlcoholScreeningExtract.Emr;
            Project = DrugAlcoholScreeningExtract.Project;
            PatientId = DrugAlcoholScreeningExtract.PatientId;
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
            // return new DrugAlcoholScreeningExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new DrugAlcoholScreeningExtract();
        }


        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string DrinkingAlcohol { get; set; }
        public string Smoking { get; set; }
        public string DrugUse { get; set; }
    }
}
