using System;
using System.Collections.Generic;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Find(Guid id);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(Guid id);
        void Execute(string sql);
        void CommitChanges();
    }
}