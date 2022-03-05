using System;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Model
{
    public class SitePatientSourceDto
    {
        public Guid FacilityId { get; set; }
        public List<int> PatientPK { get; set; } = new List<int>();
    }
}
