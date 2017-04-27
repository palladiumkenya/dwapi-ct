using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using log4net;
using PagedList;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public abstract class ClientRepository<TEntity> : IClientRepository<TEntity> where TEntity:class
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal DwapiRemoteContext Context;
        internal DbSet<TEntity> DbSet;

        protected ClientRepository(DwapiRemoteContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }
      
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
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
        

        public virtual void Execute(string sql)
        {
            Context.Database.ExecuteSqlCommand(sql);
        }

        public virtual void Refresh()
        {
         
        }


        public virtual void CommitChanges()
        {
            Context.SaveChanges();
        }

       
    }
}
