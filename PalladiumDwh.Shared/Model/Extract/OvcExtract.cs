using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class OvcExtract : Entity, IOvcExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OVCEnrollmentDate { get; set; }
        public string RelationshipToClient { get; set; }
        public string EnrolledinCPIMS { get; set; }
        public string CPIMSUniqueIdentifier { get; set; }
        public string PartnerOfferingOVCServices { get; set; }
        public string OVCExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }

        public OvcExtract()
        {
            Created = DateTime.Now;
        }

        public OvcExtract(string facilityName, int? visitId, DateTime? visitDate, DateTime? ovcEnrollmentDate, string relationshipToClient, string enrolledinCpims, string cpimsUniqueIdentifier, string partnerOfferingOvcServices, string ovcExitReason, DateTime? exitDate,
            Guid patientId, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified,string patientUUID)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            OVCEnrollmentDate = ovcEnrollmentDate;
            RelationshipToClient = relationshipToClient;
            EnrolledinCPIMS = enrolledinCpims;
            CPIMSUniqueIdentifier = cpimsUniqueIdentifier;
            PartnerOfferingOVCServices = partnerOfferingOvcServices;
            OVCExitReason = ovcExitReason;
            ExitDate = exitDate;
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
