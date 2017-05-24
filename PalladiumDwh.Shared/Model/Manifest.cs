using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.Shared.Model
{
    public class Manifest
    {
        public int SiteCode { get; set; }
        public List<int> PatientPKs { get; set; }=new List<int>();

        public bool IsValid()
        {
            return SiteCode > 0 && PatientPKs.Count > 0;
        }
    }
}
