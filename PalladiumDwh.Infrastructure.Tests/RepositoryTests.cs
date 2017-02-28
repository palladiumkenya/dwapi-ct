using System.Linq;
using Central.Core.Data;
using Central.Core.Model;
using NUnit.Framework;
using System.Collections.Generic;
using PalladiumDwh.Infrastructure.Tests;

namespace Central.Core.Tests.Data
{
    [TestFixture]
    public class RepositoryTests
    {
        private CentralContext _context;
        private List<Facility> _facilities;

        [SetUp]
        public void SetUp()
        {
            _context = new CentralContext(Effort.DbConnectionFactory.CreateTransient());

            _facilities = TestHelpers.GetTestFacilities();
            TestHelpers.CreateTestData(_context, _facilities);
        }

        [Test]
        public void should_GetAll_By_Filter()
        {
            var repository=new FacilityRepository(_context);

            var facilities = repository.GetAll(f=>f.Name.ToLower().Contains("Mombasa".ToLower()));

            Assert.That(facilities.Count(),Is.EqualTo(1));
        }

        [Test]
        public void should_GetAll()
        {
            var repository = new FacilityRepository(_context);

            var facilities = repository.GetAll();

            Assert.That(facilities.Count(), Is.GreaterThan(0));
        }
        [Test]
        public void should_Get_By_Code()
        {
            var repository = new FacilityRepository(_context);

            var facility = repository.GetFacilityIdBCode("001");

            Assert.NotNull(facility);
        }
        [Test]
        public void should_FindByKey()
        {
            var repository = new FacilityRepository(_context);

            var facility = repository.FindByKey(_facilities.First().Id);

            Assert.IsNotNull(facility);
        }

        [Test]
        public void should_Insert()
        {
            var repository = new FacilityRepository(_context);
            var newFacility= new Facility("Test", "xxx");
            repository.Insert(newFacility);
            repository.Commit();

            repository = new FacilityRepository(_context);
            var facility = repository.FindByKey(newFacility.Id);

            Assert.IsNotNull(facility);
            Assert.That(facility.Name,Is.EqualTo("Test"));
        }

        [Test]
        public void should_Insert_Range()
        {
            var repository = new FacilityRepository(_context);
            var newFacilities=new List<Facility>()
            {
                new Facility("Narok Health Center","1001"),
                new Facility("Nakuru County Referral Hospital","1002"),
            };

            repository.Insert(newFacilities);
            repository.Commit();

            var facilityA = repository.FindByKey(newFacilities[0].Id);
            var facilityB = repository.FindByKey(newFacilities[1].Id);
            Assert.IsNotNull(facilityA);
            Assert.IsNotNull(facilityB);
        }

        [Test]
        public void should_Update()
        {
            var repository = new FacilityRepository(_context);
            var facility = repository.FindByKey(_facilities.First().Id);
            facility.Code = "10011";

            repository.Update(facility);
            repository.Commit();

            var updateFacility = repository.FindByKey(_facilities.First().Id);
            Assert.IsNotNull(updateFacility);
            Assert.That(updateFacility.Code, Is.EqualTo("10011"));
        }

        [Test]
        public void should_Delete()
        {
            var repository = new FacilityRepository(_context);

            repository.Delete(_facilities.First().Id);
            repository.Commit();

            var deletedFacility = repository.FindByKey(_facilities.First().Id);
            Assert.IsNull(deletedFacility);
        }
    }
}