using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.Wapi.Repository
{
    public interface IRepository<TEntity, in TPrimaryKey> where TEntity :class
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity Get(TPrimaryKey id);
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Post(TEntity entity);
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        void Put(TPrimaryKey id, TEntity entity);
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(TPrimaryKey id);
    }
}
