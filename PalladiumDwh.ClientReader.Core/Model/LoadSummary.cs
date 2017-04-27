namespace PalladiumDwh.ClientReader.Core.Model
{
    public class LoadSummary
    {
        public int Loaded { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }

        public int ErrorCount()
        {
            var errorCount= Total - Loaded;
            return errorCount > 0 ? errorCount : 0;
        }
        public string ErrorStatus()
        {
            if (ErrorCount() > 0)
            {
                return $"| Rejected {ErrorCount()}";
            }
            return string.Empty;
        }
        public override string ToString()
        {
            return $"Loaded {Loaded} of {Total}";
        }
    }
}
