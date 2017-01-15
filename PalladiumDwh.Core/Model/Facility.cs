using System;
using System.Collections.Generic;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class Facility : Entity
    {
        public int? FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public DateTime? DateLoaded { get; set; }
        public int? UploadStatus { get; set; }
        public DateTime? DateUploaded { get; set; }
        public virtual ICollection<UploadErrorLog> UploadErrorLogs { get; set; }=new List<UploadErrorLog>();
    }
}
