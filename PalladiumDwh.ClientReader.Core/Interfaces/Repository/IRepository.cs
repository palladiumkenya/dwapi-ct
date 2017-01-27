using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : ClientExtract
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> predicate);
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