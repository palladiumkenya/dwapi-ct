namespace PalladiumDwh.DWapi.Client.Model.DTO
{
    public class FacilityDTO 
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public FacilityDTO()
        {
        }

        public FacilityDTO(Facility facility)
        {
            Code = facility.Code;
            Name = facility.Name;
        }

        public Facility GenerateFacility()
        {
            return new Facility(Code ,Name);
        }
    }
}
