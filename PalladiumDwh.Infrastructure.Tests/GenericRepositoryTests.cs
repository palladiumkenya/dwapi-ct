using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;

namespace PalladiumDwh.Infrastructure.Tests
{

    [TestFixture]
    public class GenericRepositoryTests
    {
        private DwhServerContext _context;
        private List<Facility> _facilities;

        [SetUp]
        public void SetUp()
        {
            _context = new DwhServerContext(Effort.DbConnectionFactory.CreateTransient(),true);
            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);
        }

        [Test]
        public void should_Find()
        {
            var repository = new FacilityRepository(_context);

            var facility = repository.Find(_facilities.First().Id);

            Assert.IsNotNull(facility);

            Console.WriteLine(facility);
        }
        /*
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
        public virtual void Execute(string sql)
        {
            Context.Database.ExecuteSqlCommand(sql);
        }

        public virtual void CommitChanges()
        {
            Context.SaveChanges();
        }
        */
    }
}
