using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Common;
using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Enum;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public abstract class SourceBag<T> : ISourceBag<T>
    {
        public string JobId { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public UploadMode Mode { get; set; }
        public string DwapiVersion { get; set; }
        public int SiteCode { get; set; }
        public string Facility { get; set; }
        public Guid? ManifestId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid? FacilityId { get; set; }
        public string Tag { get; set; }
        public List<T> Extracts { get; set; } = new List<T>();

        public bool HasJobId => !string.IsNullOrWhiteSpace(JobId);

        public virtual void SetFacility(List<FacilityCacheDto> facilityCacheDtos)
        {
            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : FacilityId.Value;
        }

        public override string ToString()
        {
            return $"{SiteCode}-{Facility} [{Extracts.Count}] {typeof(T).Name.Replace($"{nameof(SourceDto)}","")}";
        }
    }
}
