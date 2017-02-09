namespace PalladiumDwh.ClientReader.Core.Model
{
    public class RunSummary
    {
        public LoadSummary LoadSummary { get; set; }
        public SyncSummary SyncSummary { get; set; }

        public RunSummary()
        {
            LoadSummary=new LoadSummary();
            SyncSummary=new SyncSummary();
        }

        public override string ToString()
        {
            return $"Loaded {SyncSummary.Total} of {LoadSummary.Total} {LoadSummary.ErrorStatus()}";
        }
    }
}