using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model
{
    public class MasterFacility
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }
        public string Name { get; set; }
        public string County { get; set; }
        public Guid? FacilityId { get; set; }

        public DateTime? SnapshotDate { get; set; }
        public int? SnapshotSiteCode { get; set; }
        public int? SnapshotVersion { get; set; }
        [NotMapped]
        public Guid? SessionId { get; set; }
        [NotMapped]
        public Guid? ManifestId { get; set; }
        [NotMapped]
        public string JobId { get; set; }
        public MasterFacility()
        {
        }
        public MasterFacility(int code, string name,string county)
        {
            Code = code;
            Name = name;
            County = county;
        }

        public static MasterFacility GenFacility()
        {
            return new MasterFacility(-1, "x", "x");
        }

        public MasterFacility TakeSnap(List<MasterFacility> mflSnaps)
        {
            MasterFacility lastSnap = null;

            if (mflSnaps.Any())
                lastSnap = mflSnaps
                    .OrderBy(x => x.SnapshotDate)
                    .ThenBy(x=>x.SnapshotVersion)
                    .Last();

            var snapVersion = null == lastSnap ? 1 : lastSnap.GetNextSnapshotVersion();

            var snapSiteCode = Convert.ToInt32($"-{100 + snapVersion}{Code}");

            var fac = this;
            fac.SnapshotSiteCode = Code;
            fac.Code =snapSiteCode;
            fac.SnapshotDate = DateTime.Now;
            fac.SnapshotVersion = snapVersion;
            return fac;
        }

        private int GetNextSnapshotVersion()
        {
            if (SnapshotVersion.HasValue)
                return SnapshotVersion.Value + 1;

            return 0;
        }
        public override string ToString()
        {
            return $"{Code} - {Name} ({County})";
        }
    }
}
