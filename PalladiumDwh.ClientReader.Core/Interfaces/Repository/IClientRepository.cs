using System;
using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IClientRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
        void Execute(string sql);
        void Refresh();
        void CommitChanges();

    }
}