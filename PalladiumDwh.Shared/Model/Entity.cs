using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.Shared.Model
{

    public abstract class Entity 
    {
        [Key]
        public virtual Guid Id { get; set; }
        [Column(Order = 100)]
        public virtual string Emr { get; set; }
        [Column(Order = 101)]
        public virtual string Project { get; set; }
        [Column(Order = 102)]
        public virtual bool Voided { get; set; }
        [Column(Order = 103)]
        public virtual bool Processed { get; set; }


        protected Entity(Guid id)
        {
            Id = id;
        }
        protected Entity()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}
