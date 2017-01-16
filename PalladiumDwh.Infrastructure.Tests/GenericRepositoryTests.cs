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
        private Facility _facilityA;
        private List<Facility> _facilities;
        private List<PatientExtract> _patients;

        [SetUp]
        public void SetUp()
        {
            _context = new DwhServerContext(Effort.DbConnectionFactory.CreateTransient(),true);
            

            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);

            _facilityA = _facilities.First();
            _patients = TestHelpers.GetTestPatientVisitsData(_facilityA, 2, 10).ToList();
            TestHelpers.CreateTestData(_context,_patients);

        }

        [Test]
        public void should_Find()
        {
            var repository = new FacilityRepository(_context);

            var facility = repository.Find(_facilities.First().Id);

            Assert.IsNotNull(facility);

            Console.WriteLine(facility);
        }

        [Test]
        public void should_Insert()
        {
            var newFacility=new Facility() {Code = 1,Name = "Maun Hosptial"};
            var repository = new FacilityRepository(_context);

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
            _context = new DwhServerContext(Effort.DbConnectionFactory.CreateTransient(), true);
            var newFacilityList = TestHelpers.GetTestData<Facility>(20);
            var repository = new FacilityRepository(_context);

            repository.Insert(newFacilityList);
            repository.CommitChanges();

            var facility = repository.Find(newFacilityList.Last().Id);

            Assert.IsNotNull(facility);

            Console.WriteLine(facility);
        }

        
        [Test]
        public void should_Update()
        {
            var repository = new FacilityRepository(_context);

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
            var repository = new FacilityRepository(_context);
            var facility = _facilities.First();
            repository.Delete(facility.Id);
            repository.CommitChanges();
            
            var deletedFacility = repository.Find(facility.Id);

            Assert.IsNull(deletedFacility);
        }

        [Test]
        public void should_DeleteBy()
        {
            var patient = _facilityA.PatientExtracts.First();
            var repository = new PatientExtractRepository(_context);
            var patientA=repository.Find(patient.Id);
            Assert.IsNotNull(patientA);
            Assert.That(patientA.PatientVisitExtracts.Count,Is.GreaterThan(1));

            var vistRepo=new PatientVisitRepository(_context);
            vistRepo.Clear(patientA.Id);
            vistRepo.CommitChanges();



            patientA = repository.Find(patient.Id);
            Assert.IsNotNull(patientA);
            Assert.That(patientA.PatientVisitExtracts.Count, Is.EqualTo(0));
        }
        /*
        [Test]
        public void should_Execute()
        {
            var facility = _facilities.First();

            var repository = new FacilityRepository(_context);
            repository.Execute("delete from Facility");
            repository.CommitChanges();

            var deletedfacility = repository.Find(facility.Id);

            Assert.IsNull(deletedfacility);
        } 
        */
    }
}
