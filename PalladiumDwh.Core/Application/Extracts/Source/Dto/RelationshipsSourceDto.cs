using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Core.Application.Extracts.Source.Dto
{
    public class RelationshipsSourceDto:SourceDto, IRelationships
    {
        public string FacilityName { get; set; }

        public string RelationshipToPatient { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }

        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }

    }
}