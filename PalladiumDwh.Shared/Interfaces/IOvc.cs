using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IOvc
    {
         string FacilityName { get; set; }
         int ? VisitID { get; set; }
         DateTime ? VisitDate { get; set; }
         DateTime ? OVCEnrollmentDate { get; set; }
         string RelationshipToClient { get; set; }
         string EnrolledinCPIMS { get; set; }
         string CPIMSUniqueIdentifier { get; set; }
         string PartnerOfferingOVCServices { get; set; }
         string OVCExitReason { get; set; }
         DateTime ? ExitDate { get; set; }
    }
}
