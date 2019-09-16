using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Custom;

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

        public string GetInitExtracts(Guid facilityId)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            int total = PatientPKs.Count;

            foreach (var patientPk in PatientPKs)
            {
                count++;
                sb.AppendLine($"SELECT '{LiveGuid.NewGuid()}' as Id,{patientPk} as PatientPID,'{facilityId}' as FacilityID {(count == total ? "" : "UNION")}");
            }

            var sql = $@"
INSERT INTO PatientExtract(Id,PatientPID,FacilityID,Voided,Processed,Created)
SELECT 
	e.Id, e.PatientPID,e.FacilityID,0,0,GETDATE()
FROM            
	(
    {sb}
    ) AS e LEFT OUTER JOIN PatientExtract AS p ON e.PatientPID = p.PatientPID
WHERE        
	(p.Id IS NULL)
";
            return sql;
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
