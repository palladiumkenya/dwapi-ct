using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Model.Bag
{
    public class PatientSourceBag : ISourceBag<PatientSourceDto>
    {
        public  EmrSetup EmrSetup { get; set; }
        public  UploadMode Mode  { get; set; }
        public string DwapiVersion { get; set; }
        public int SiteCode { get; set; }
        public string Facility { get; set; }
        public Guid? ManifestId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid? FacilityId { get; set; }
        public string Tag { get; set; }
        public List<PatientSourceDto> Extracts { get; set; } = new List<PatientSourceDto>();
    }
}
