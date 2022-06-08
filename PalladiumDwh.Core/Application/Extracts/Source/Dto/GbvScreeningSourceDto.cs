using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Core.Application.Extracts.Source.Dto
{
    public class GbvScreeningSourceDto:SourceDto, IGbvScreening
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string IPV { get; set; }
        public string PhysicalIPV { get; set; }
        public string EmotionalIPV { get; set; }
        public string SexualIPV { get; set; }
        public string IPVRelationship { get; set; }
    }
}
