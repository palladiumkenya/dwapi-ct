using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PalladiumDwh.ClientReader.Core.Model
{
    [Table("Facility")]
    public class ClientFacility 
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }

        public ClientFacility()
        {
            
        }
        public ClientFacility(int code, string name,string emr, string project)
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
