using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class PushResponse
    {
        public int PatientPK { get; set; }
        public int SiteCode { get; set; }
        public string Ack{ get; set; }
        public bool IsSuccess { get;  }

        public PushResponse(IClientExtractProfile profile,string message,bool success)
        {
            PatientPK = profile.Demographic.PatientPID;
            SiteCode = profile.Demographic.FacilityId;
            Ack = message;
            IsSuccess = success;
        }

        public override string ToString()
        {
            return $"{SiteCode}-{PatientPK}|{Ack}";
        }
    }
}