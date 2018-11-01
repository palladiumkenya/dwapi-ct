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

        public int PatientCount => PatientPKs.Count;
        public string Items => string.Join(",", PatientPKs);

        public Manifest()
        {
        }

        public Manifest(int siteCode)
        {
            SiteCode = siteCode;
        }
   
        public bool IsValid()
        {
            return SiteCode > 0 && PatientPKs.Count > 0;
        }
        public void AddPatientPk(int pk)
        {
            if(!PatientPKs.Contains(pk))
            PatientPKs.Add(pk);
        }
        public string GetPatientPKsJoined()
        {
            return string.Join(",", PatientPKs);
        }
        public override string ToString()
        {
            return $"{SiteCode} AllowedToSend ({PatientPKs.Count})";
        }
    }
}
