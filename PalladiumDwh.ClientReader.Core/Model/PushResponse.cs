using System;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class PushResponse
    {
        public int PatientPK { get; set; }
        public int SiteCode { get; set; }
        public string QueueId{ get; set; }
        public string Status { get; set; }
        public bool IsSuccess { get;  }
        public DateTime StatusDate { get; set; }

        public PushResponse(IClientExtractProfile profile,string message, string status,bool success)
        {
            PatientPK = profile.Demographic.PatientPID;
            SiteCode = profile.Demographic.FacilityId;
            QueueId = message;
            Status = status;
            IsSuccess = success;
            StatusDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{SiteCode}-{PatientPK}|{QueueId}";
        }
    }
}