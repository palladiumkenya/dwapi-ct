using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Model
{

    public abstract class ClientExtract:IClientExtract
    {
        public  virtual  int PatientPK { get; set; }
        public  virtual  int SiteCode { get; set; }

        [Column(Order = 100)]
        public virtual string Emr { get; set; }
        [Column(Order = 101)]
        public virtual string Project { get; set; }
        [Column(Order = 103)]
        public virtual bool Processed { get; set; }
        public virtual Guid UId { get; set; }

        protected ClientExtract()
        {
            UId = Guid.NewGuid();
        }
    }
}
