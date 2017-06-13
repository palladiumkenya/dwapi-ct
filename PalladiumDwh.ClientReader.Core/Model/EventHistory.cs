using System;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Model
{

    public class EventHistory : Entity
    {
        public int SiteCode { get; set; }
        public string Display { get; set; }

        public int? Found { get; set; }
        public DateTime? FoundDate { get; set; }
        public string FoundStatus { get; set; }
        public bool IsFoundSuccess { get; set; }

        public int? Loaded { get; set; }
        public int? Rejected { get; set; }
        public DateTime? LoadDate { get; set; }
        public string LoadStatus { get; set; }
        public bool IsLoadSuccess { get; set; }

        public int? Sent { get; set; }
        public int? NotSent { get; set; }
        public DateTime? SendDate { get; set; }
        public string SendStatus { get; set; }
        public bool IsSendSuccess { get; set; }

        public Guid ExtractSettingId { get; set; }

        [NotMapped]
        public override string Emr { get; set; }

        [NotMapped]
        public override string Project { get; set; }

        public string FoundInfo()
        {
            return $"Found {Found} {FoundDate.GetTiming("|")}";
        }

        public string LoadInfo()
        {
            return $"Loaded {Loaded}/{Found} {LoadDate.GetTiming("|")}";
        }

        public string SendInfo()
        {
            return $"Sent {Sent}/{NotSent} {SendDate.GetTiming("|")}";
        }
    }
}