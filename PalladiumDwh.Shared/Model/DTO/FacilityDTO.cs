namespace PalladiumDwh.Shared.Model.DTO
{
    public class FacilityDTO 
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }

        public FacilityDTO()
        {
        }

        public FacilityDTO(int code, string name, string emr, string project)
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }

        public FacilityDTO(Facility facility)
        {
            Code = facility.Code;
            Name = facility.Name;
            Emr = facility.Emr;
            Project = facility.Project;
        }

        public Facility GenerateFacility()
        {
            return new Facility(Code ,Name,Emr,Project);
        }

        public bool IsValid()
        {
            return Code > 0;
        }
    }
}
