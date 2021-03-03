using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class ContactListingExtractDTO : IContactListingExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public ContactListingExtractDTO()
        {
        }

        public ContactListingExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public ContactListingExtractDTO(ContactListingExtract ContactListingExtract)
        {
            // ExitDescription = ContactListingExtract.ExitDescription;
            // ExitDate = ContactListingExtract.ExitDate;
            // ExitReason = ContactListingExtract.ExitReason;
            Emr = ContactListingExtract.Emr;
            Project = ContactListingExtract.Project;
            PatientId = ContactListingExtract.PatientId;
        }



        public IEnumerable<ContactListingExtractDTO> GenerateContactListingExtractDtOs(IEnumerable<ContactListingExtract> extracts)
        {
            var statusExtractDtos = new List<ContactListingExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new ContactListingExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public ContactListingExtract GenerateContactListingExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new ContactListingExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new ContactListingExtract();
        }


        public string FacilityName { get; set; }
        public int? PartnerPersonID { get; set; }
        public string ContactAge { get; set; }
        public string ContactSex { get; set; }
        public string ContactMaritalStatus { get; set; }
        public string RelationshipWithPatient { get; set; }
        public string ScreenedForIpv { get; set; }
        public string IpvScreening { get; set; }
        public string IPVScreeningOutcome { get; set; }
        public string CurrentlyLivingWithIndexClient { get; set; }
        public string KnowledgeOfHivStatus { get; set; }
        public string PnsApproach { get; set; }
    }
}
