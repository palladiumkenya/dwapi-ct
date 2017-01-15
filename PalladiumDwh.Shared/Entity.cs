using System;

namespace PalladiumDwh.Shared
{
    public abstract class Entity
    {
        public virtual Guid Id { get; set; }

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
