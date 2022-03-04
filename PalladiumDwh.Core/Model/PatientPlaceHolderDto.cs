using System;

namespace PalladiumDwh.Core.Model
{
    public class PatientPlaceHolderDto
    {
        public Guid Id { get; set; }
        public int SiteCode { get; set; }
        public int PatientPK { get; set; }
        public Guid FacilityId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
