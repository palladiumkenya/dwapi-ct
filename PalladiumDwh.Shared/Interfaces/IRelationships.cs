using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IRelationships
    {
        string FacilityName { get; set; }
        string RelationshipToPatient { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        string RecordUUID { get; set; }
        bool Voided { get; set; }
        
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }

    }
}
