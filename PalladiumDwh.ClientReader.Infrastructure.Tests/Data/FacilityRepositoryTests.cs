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
        public void should_Clear_all()
        {
            _facilityRepository.Clear();

            var facilities = _facilityRepository.GetAll();

            Assert.That(facilities,Is.Empty);
        }
       
        [Test]
        public void should_Sync()
        {
            var newFacilities = Builder<Facility>.CreateListOfSize(3).Build().ToList();
            var newIds = newFacilities.Select(x => x.Id).ToList();

            _facilityRepository.Sync(newFacilities);

            

            var savedNew = _facilityRepository.GetAll().Where(x => newIds.Contains(x.Id)).ToList();


            Assert.IsTrue(savedNew.Count>0);
        }
    }
}