using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Infrastructure.Tests
{
    [TestFixture]
    public class FacilityDapperRepositoryTests
    {
        private DwapiCentralContext _context;
        private List<Facility> _facilities;
        private List<Guid> _deleteIds=new List<Guid>();
        private IFacilityRepository _facilityRepository;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext();
            _facilities = new List<Facility>
            {
                new Facility(22704, @"Meditrust HCS", "IQCare", "Kenya HMIS II"),
                new Facility(22696, @"Meditrust HCS", "IQCare", "Kenya HMIS II"),
                new Facility(22691, @"Meditrust HCS", "IQCare", "Kenya HMIS II")
            };

            //_facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);
            _facilityRepository = new FacilityRepository(_context);
        }

        [Test]
        public void should_GetFacilityIdByCode()
        {
            var id = _facilityRepository.GetFacilityIdByCode(_facilities.First().Code);

            Assert.IsNotNull(id);
            Console.WriteLine($"Facility ID:{id}");
            Assert.IsTrue(id != Guid.Empty);
        }

        [Test]
        public void should_GetFacilityIdByCode_HandleNull()
        {
            var id = _facilityRepository.GetFacilityIdByCode(12);

            Assert.IsNull(id);
        }





        [Test]
        public void should_Sync_New()
        {
            var newFacility = Builder<Facility>.CreateNew().Build();
            newFacility.Code = DateTime.UtcNow.Millisecond;

            _facilityRepository.SyncNew(newFacility);
            _facilities.Add(newFacility);

            var saved = _facilityRepository.Find(newFacility.Id);
            Assert.IsNotNull(saved);
            Console.WriteLine(saved);
            Assert.AreEqual(saved.Code,newFacility.Code);
            Assert.AreEqual(saved.Name, newFacility.Name);
            _deleteIds.Add(newFacility.Id);
        }

        [Test]
        public void should_Sync_Exisitng()
        {
            var facility = _facilities.Last();

            var newFacility = Builder<Facility>.CreateNew().Build();
            newFacility.Code = facility.Code;

            _facilityRepository.SyncNew(newFacility);

            var saved = _facilityRepository.Find(x=>x.Code==newFacility.Code);
            Assert.IsNotNull(saved);
            Console.WriteLine(saved);
            Assert.AreEqual(saved.Code, newFacility.Code);
            Assert.AreNotEqual(saved.Id, newFacility.Id);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var facility in _facilities)
            {
                _deleteIds.Add(facility.Id);
            }
            
            if (_deleteIds.Count > 0)
            {
                var ids=new List<string>();
                foreach (var deleteId in _deleteIds)
                {
                    ids.Add($"'{deleteId}'");
                }
                _context.Database.ExecuteSqlCommand($"DELETE FROM Facility WHERE Id IN ({string.Join(",",ids)})");
            }
        }
    }
}