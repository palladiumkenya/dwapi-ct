using System.Collections.Generic;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model.DTO
{
    public class FacilityDTO 
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public FacilityDTO()
        {
        }

        public FacilityDTO(int code, string name)
        {
            Code = code;
            Name = name;
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
