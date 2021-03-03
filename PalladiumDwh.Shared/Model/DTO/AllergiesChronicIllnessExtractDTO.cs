using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class AllergiesChronicIllnessExtractDTO : IAllergiesChronicIllnessExtractDTO
    {

        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string ChronicIllness { get; set; }
        public DateTime? ChronicOnsetDate { get; set; }
        public string knownAllergies { get; set; }
        public string AllergyCausativeAgent { get; set; }
        public string AllergicReaction { get; set; }
        public string AllergySeverity { get; set; }
        public DateTime? AllergyOnsetDate { get; set; }
        public string Skin { get; set; }
        public string Eyes { get; set; }
        public string ENT { get; set; }
        public string Chest { get; set; }
        public string CVS { get; set; }
        public string Abdomen { get; set; }
        public string CNS { get; set; }
        public string Genitourinary { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public AllergiesChronicIllnessExtractDTO()
        {
        }

        public AllergiesChronicIllnessExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public AllergiesChronicIllnessExtractDTO(AllergiesChronicIllnessExtract AllergiesChronicIllnessExtract)
        {
            // ExitDescription = AllergiesChronicIllnessExtract.ExitDescription;
            // ExitDate = AllergiesChronicIllnessExtract.ExitDate;
            // ExitReason = AllergiesChronicIllnessExtract.ExitReason;
            Emr = AllergiesChronicIllnessExtract.Emr;
            Project = AllergiesChronicIllnessExtract.Project;
            PatientId = AllergiesChronicIllnessExtract.PatientId;
        }



        public IEnumerable<AllergiesChronicIllnessExtractDTO> GenerateAllergiesChronicIllnessExtractDtOs(IEnumerable<AllergiesChronicIllnessExtract> extracts)
        {
            var statusExtractDtos = new List<AllergiesChronicIllnessExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new AllergiesChronicIllnessExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public AllergiesChronicIllnessExtract GenerateAllergiesChronicIllnessExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new AllergiesChronicIllnessExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new AllergiesChronicIllnessExtract();
        }

    }
}
