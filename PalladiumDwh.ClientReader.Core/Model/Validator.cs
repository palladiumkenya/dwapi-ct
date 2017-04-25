using System;
using System.Collections.Generic;
using PalladiumDwh.ClientValidator.Core.Interfaces;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class Validator:IValidator
    {
        public Guid Id { get; set; }
        public string Extract { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Logic { get; set; }
        public string Summary { get; set; }     
        public virtual ICollection<ValidationError> ValidationErrors { get; set; }=new List<ValidationError>();

        public Validator()
        {
            Id = LiveGuid.NewGuid();
        }

        public string GenerateValidateSQL()
        {
            return "";
        }
    }
}