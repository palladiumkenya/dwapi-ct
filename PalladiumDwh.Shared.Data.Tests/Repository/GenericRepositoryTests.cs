using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Data.Tests.Repository
{
    class TestDbContext : DwapiBaseContext
    {
        public TestDbContext(string connection) : base(connection)
        {
        }

        public TestDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public virtual DbSet<Facility> Facilities { get; set; }
    }

    class TestFacilityRepository : GenericRepository<Facility>
    {
        private readonly TestDbContext _context;

        public TestFacilityRepository(TestDbContext context) : base(context)
        {
            _context = context;
        }
    }

    [TestFixture]
    public class GenericRepositoryTests
    {
        private TestDbContext _context;
        private Facility _facilityA;
        private List<Facility> _facilities;
        private List<PatientExtract> _patients;

        [SetUp]
        public void SetUp()
        {
            _context = new TestDbContext(Effort.DbConnectionFactory.CreateTransient(),true);
            

            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            _facilities.First().Name = "Maun Facility";
            _facilities.Last().Name = "Hospital Maun";
            TestHelpers.CreateTestData(_context, _facilities);

            _facilityA = _facilities.First();
            _patients = TestHelpers.GetTestPatientVisitsData(_facilityA, 2, 10).ToList();
            TestHelpers.CreateTestData(_context,_patients);

        }
        [Test]
        public void should_GetAll()
        {
            var repository = new TestFacilityRepository(_context);

            var facilities = repository.GetAll().ToList();

            Assert.IsTrue(facilities.Count>0);
            foreach (var f in facilities)
            {
                Console.WriteLine(f);
            }
            
        }
        [Test]
        public void should_GetAll_by_Expression()
        {
            var repository = new TestFacilityRepository(_context);

            var facilities = repository.GetAllBy(x=>x.Name.ToLower().Contains("aun".ToLower())).ToList();

            Assert.IsTrue(facilities.Count > 0);
            foreach (var f in facilities)
            {
                Console.WriteLine(f);
            }
        }
        [Test]
        public void should_Find()
        {
            var repository = new TestFacilityRepository(_context);

            var facility = repository.Find(_facilities.First().Id);

            Assert.IsNotNull(facility);

            Console.WriteLine(facility);
        }
        [Test]
        public void should_Find_by_Expression()
        {
            var repository = new TestFacilityRepository(_context);
            var name = _facilities.First().Name;
            var facility = repository.Find(x=>x.Name.Equals(name));

            Assert.IsNotNull(facility);

            Console.WriteLine(facility);
        }

        [Test]
        public void should_Insert()
        {
            var newFacility=new Facility() {Code = 1,Name = "Maun Hosptial"};
            var repository = new TestFacilityRepository(_context);

            repository.Insert(newFacility);
            repository.CommitChanges();

            var facility = repository.Find(newFacility.Id);

            Assert.IsNotNull(facility);
            Assert.AreEqual(1,facility.Code);
            Assert.AreEqual("Maun Hosptial", facility.Name);

            Console.WriteLine(facility);
        }

        
        [Test]
        public void should_Insert_Range()
        {
            _context = new TestDbContext(Effort.DbConnectionFactory.CreateTransient(), true);
            var newFacilityList = TestHelpers.GetTestData<Facility>(20);
            var repository = new TestFacilityRepository(_context);

            repository.Insert(newFacilityList);
            repository.CommitChanges();

            var facility = repository.Find(newFacilityList.Last().Id);

            Assert.IsNotNull(facility);

            Console.WriteLine(facility);
        }

        
        [Test]
        public void should_Update()
        {
            var repository = new TestFacilityRepository(_context);

            var facility = repository.Find(_facilities.First().Id);
            facility.Name = "New Maun";
            repository.Update(facility);
            repository.CommitChanges();


            var savedFacility = repository.Find(facility.Id);

            Assert.IsNotNull(savedFacility);
            Assert.AreEqual("New Maun", savedFacility.Name);
            Console.WriteLine(savedFacility);
        }

        [Test]
        public void should_Delete()
        {
            var repository = new TestFacilityRepository(_context);
            var facility = _facilities.First();
            repository.Delete(facility.Id);
            repository.CommitChanges();
            
            var deletedFacility = repository.Find(facility.Id);

            Assert.IsNull(deletedFacility);
        }

        [Test]
        public void should_DeleteBy()
        {
            var repository = new TestFacilityRepository(_context);
            var facility = _facilities.First();
            var code = _facilities.First().Code;
            repository.DeleteBy(x => x.Code == code);
            repository.CommitChanges();

            var deletedFacility = repository.Find(facility.Id);

            Assert.IsNull(deletedFacility);
        }
        /*
        [Test]
        public void should_Execute()
        {
            var facility = _facilities.First();

            var repository = new TestFacilityRepository(_context);
            repository.Execute("delete from Facility");
            repository.CommitChanges();

            var deletedfacility = repository.Find(facility.Id);

            Assert.IsNull(deletedfacility);
        } 
        */

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
