using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class UploadErrorLog:Entity
    {
        public string ErrorDescription { get; set; }
        public int? FacilityId { get; set; }    
        public virtual Facility Facility { get; set; }
    }
}
