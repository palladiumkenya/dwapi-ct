using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        protected Entity(Guid id)
        {
            Id = id;
        }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
