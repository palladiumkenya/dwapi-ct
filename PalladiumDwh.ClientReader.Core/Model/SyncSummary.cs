namespace PalladiumDwh.ClientReader.Core.Model
{
    public class SyncSummary
    {
        public int Total { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Loaded {Total}";
        }
    }
}
