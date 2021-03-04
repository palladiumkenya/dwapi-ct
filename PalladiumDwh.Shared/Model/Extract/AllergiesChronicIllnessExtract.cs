using System;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class AllergiesChronicIllnessExtract : Entity, IAllergiesChronicIllnessExtract
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
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }

        public AllergiesChronicIllnessExtract()
        {
            Created = DateTime.Now;
        }

        public AllergiesChronicIllnessExtract(string facilityName, int? visitId, DateTime? visitDate, string chronicIllness, DateTime? chronicOnsetDate, string knownAllergies, string allergyCausativeAgent, string allergicReaction, string allergySeverity, DateTime? allergyOnsetDate, string skin, string eyes, string ent, string chest, string cvs, string abdomen, string cns, string genitourinary,
            Guid patientId, string emr, string project)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            ChronicIllness = chronicIllness;
            ChronicOnsetDate = chronicOnsetDate;
            this.knownAllergies = knownAllergies;
            AllergyCausativeAgent = allergyCausativeAgent;
            AllergicReaction = allergicReaction;
            AllergySeverity = allergySeverity;
            AllergyOnsetDate = allergyOnsetDate;
            Skin = skin;
            Eyes = eyes;
            ENT = ent;
            Chest = chest;
            CVS = cvs;
            Abdomen = abdomen;
            CNS = cns;
            Genitourinary = genitourinary;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }
    }
}
