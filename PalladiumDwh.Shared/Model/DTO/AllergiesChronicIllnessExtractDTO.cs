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
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public AllergiesChronicIllnessExtractDTO()
        {
        }

        public AllergiesChronicIllnessExtractDTO(AllergiesChronicIllnessExtract AllergiesChronicIllnessExtract)
        {
            FacilityName=AllergiesChronicIllnessExtract.FacilityName;
            VisitID=AllergiesChronicIllnessExtract.VisitID;
            VisitDate=AllergiesChronicIllnessExtract.VisitDate;
            ChronicIllness=AllergiesChronicIllnessExtract.ChronicIllness;
            ChronicOnsetDate=AllergiesChronicIllnessExtract.ChronicOnsetDate;
            knownAllergies=AllergiesChronicIllnessExtract.knownAllergies;
            AllergyCausativeAgent=AllergiesChronicIllnessExtract.AllergyCausativeAgent;
            AllergicReaction=AllergiesChronicIllnessExtract.AllergicReaction;
            AllergySeverity=AllergiesChronicIllnessExtract.AllergySeverity;
            AllergyOnsetDate=AllergiesChronicIllnessExtract.AllergyOnsetDate;
            Skin=AllergiesChronicIllnessExtract.Skin;
            Eyes=AllergiesChronicIllnessExtract.Eyes;
            ENT=AllergiesChronicIllnessExtract.ENT;
            Chest=AllergiesChronicIllnessExtract.Chest;
            CVS=AllergiesChronicIllnessExtract.CVS;
            Abdomen=AllergiesChronicIllnessExtract.Abdomen;
            CNS=AllergiesChronicIllnessExtract.CNS;
            Genitourinary=AllergiesChronicIllnessExtract.Genitourinary;
            Emr=AllergiesChronicIllnessExtract.Emr;
            Project=AllergiesChronicIllnessExtract.Project;
            PatientId=AllergiesChronicIllnessExtract.PatientId;
            Date_Created=AllergiesChronicIllnessExtract.Date_Created;
            Date_Last_Modified=AllergiesChronicIllnessExtract.Date_Last_Modified;

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
            return new AllergiesChronicIllnessExtract(
                FacilityName,
                VisitID,
                VisitDate,
                ChronicIllness,
                ChronicOnsetDate,
                knownAllergies,
                AllergyCausativeAgent,
                AllergicReaction,
                AllergySeverity,
                AllergyOnsetDate,
                Skin,
                Eyes,
                ENT,
                Chest,
                CVS,
                Abdomen,
                CNS,
                Genitourinary,
                PatientId,
                Emr,
                Project,
                Date_Created,
                Date_Last_Modified
            );
        }

    }
}
