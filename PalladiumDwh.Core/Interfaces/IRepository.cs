using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Find(Guid id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(Guid id);
        void DeleteBy(Expression<Func<TEntity, bool>> predicate);
        void Execute(string sql);
        void CommitChanges();
    }
}