namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public class ClientFacilityDTO 
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }

        public ClientFacilityDTO()
        {
        }

        public ClientFacilityDTO(int code, string name, string emr, string project)
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }

        public ClientFacilityDTO(ClientFacility facility)
        {
            Code = facility.Code;
            Name = facility.Name;
            Emr = facility.Emr;
            Project = facility.Project;
        }

        public ClientFacility GenerateFacility()
        {
            return new ClientFacility(Code ,Name,Emr,Project);
        }

        public bool IsValid()
        {
            return Code > 0;
        }
    }
}
