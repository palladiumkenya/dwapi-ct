using System.Collections.Generic;

namespace PalladiumDwh.Uploader.Repository
{
    public interface IRepository<TEntity, in TPrimaryKey> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TPrimaryKey id);
        void Post(TEntity entity);
        void Put(TPrimaryKey id,  TEntity entity);
        void Delete(TPrimaryKey id);
    }
}


