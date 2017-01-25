using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data
{
    [TestFixture]
    public class FacilityRepositoryTests
    {
        private DwapiRemoteContext _context;
        private List<Facility> _facilities;
        private IFacilityRepository _facilityRepository;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);
            _facilityRepository=new FacilityRepository(_context);
        }

        [Test]
        public void should_GetFacilityIdBCode()
        {
            var id = _facilityRepository.GetFacilityIdBCode(_facilities.First().Code);

            Assert.IsNotNull(id);
            Assert.IsTrue(id != Guid.Empty);
        }

        [Test]
        public void should_Sync_New()
        {
            var newFacility = Builder<Facility>.CreateNew().Build();
            newFacility.Code = DateTime.UtcNow.Millisecond;

            _facilityRepository.Sync(newFacility);

            var saved = _facilityRepository.Find(newFacility.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.Code,newFacility.Code);
            Assert.AreEqual(saved.Name, newFacility.Name);
        }

        [Test]
        public void should_Sync_Exisitng()
        {
            var facility = _facilities.Last();

            var newFacility = Builder<Facility>.CreateNew().Build();
            newFacility.Code = facility.Code;
            newFacility.Name = "Maun Kisumu";
            _facilityRepository.Sync(newFacility);

            var saved = _facilityRepository.Find(x=>x.Code==newFacility.Code);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.Code, newFacility.Code);
            Assert.AreEqual("Maun Kisumu", newFacility.Name);
            Assert.AreNotEqual(saved.Id, newFacility.Id);
        }
    }
}