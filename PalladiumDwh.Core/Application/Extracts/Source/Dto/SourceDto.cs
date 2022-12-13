using Newtonsoft.Json;
using System;

namespace PalladiumDwh.Core.Application.Extracts.Source.Dto
{
    public abstract class SourceDto
    {
        public Guid Id { get; set; }
        public int SiteCode { get; set; }
        public int PatientPK { get; set; }
        public DateTime DateExtracted { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }


        
        public Guid? PatientId { get; set; }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPK > 0;
        }
    }

    
}
