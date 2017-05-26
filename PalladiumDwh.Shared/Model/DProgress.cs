using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.Shared.Model
{
    public class DProgress
    {
        public string Status { get; set; }
        public int? ValuePercentage { get; set; }

        public DProgress()
        {
        }

        private DProgress(string status)
        {
            Status = status;
        }
        private DProgress(string status, int valuePercentage):this(status)
        {
            ValuePercentage = valuePercentage;
        }

        public static DProgress Report(string status)
        {
            return new DProgress(status);
        }
        public static DProgress Report(string status, int valuePercentage)
        {
            return new DProgress(status,valuePercentage);
        }

        public string ShowProgress()
        {
            return ToString();
        }

        public override string ToString()
        {
            string valueOutput = ValuePercentage.HasValue ? $"{ValuePercentage} %" :string.Empty;

            return $@"{Status} {valueOutput}";
        }
    }
}
