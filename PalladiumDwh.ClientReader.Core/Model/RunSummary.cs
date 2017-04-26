using System;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class RunSummary
    {
        public ExtractSetting ExtractSetting { get; set; }

        public LoadSummary LoadSummary { get; set; }
        public ValidationSummary ValidationSummary { get; set; }
        public SyncSummary SyncSummary { get; set; }

        public RunSummary()
        {
            LoadSummary=new LoadSummary();
            ValidationSummary=new ValidationSummary();
            SyncSummary=new SyncSummary();
        }

        public override string ToString()
        {
            return $"Loaded {SyncSummary.Total} of {LoadSummary.Total} | Rejected:{ValidationSummary.Total}";
        }

        public string Report()
        {
            return $"Loaded:{LoadSummary.Total} | Synced:{SyncSummary.Total} | Errors:{ValidationSummary.Total}";
        }
    }
}