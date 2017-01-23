using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using log4net;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Data.Repository
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal DbContext Context;
        internal DbSet<TEntity> DbSet;

        protected GenericRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual TEntity Find(Guid id)
        {
            //consider using DbSet.AsNoTracking() **
            return DbSet.Find(id);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList().FirstOrDefault();
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual void DeleteBy(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                var results = DbSet.Where(predicate);
                if (null != results.FirstOrDefault())
                {
                    DbSet.RemoveRange(results);
                }
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                throw;
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public virtual void Execute(string sql)
        {
            Context.Database.ExecuteSqlCommand(sql);
        }


        public virtual void CommitChanges()
        {
            Context.SaveChanges();
        }
    }
}
