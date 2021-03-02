using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public   interface IGbvScreening
    {
         string FacilityName { get; set; }
         int ? VisitID { get; set; }
         DateTime ? VisitDate { get; set; }
         string IPV { get; set; }
         string PhysicalIPV { get; set; }
         string EmotionalIPV { get; set; }
         string SexualIPV { get; set; }
         string IPVRelationship { get; set; }
    }
}
