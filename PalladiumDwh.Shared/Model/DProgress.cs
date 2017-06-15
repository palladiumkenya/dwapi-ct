using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.Shared.Model
{
    public class DProgress
    { 
        public object ValueObject { get; set; }
        public string Status { get; set; }
        public int? ValuePercentage { get; set; }

        public DProgress()
        {
        }

        private DProgress(string status, object valueObject=null)
        {
            Status = status;
            ValueObject = valueObject;
        }
        private DProgress(string status, int valuePercentage, object valueObject = null) :this(status,valueObject)
        {
            ValuePercentage = valuePercentage;
        }

        public static DProgress Report(string status, object valueObject = null)
        {
            return new DProgress(status,valueObject);
        }
        public static DProgress Report(string status, int valuePercentage, object valueObject = null)
        {
            return new DProgress(status,valuePercentage,valueObject);
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
