using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
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
        Task<int> DeleteByAsync(Expression<Func<TEntity, bool>> predicate);
        void Execute(string sql);
        void CommitChanges();
        Task<int> CommitChangesAsync();
    }
}