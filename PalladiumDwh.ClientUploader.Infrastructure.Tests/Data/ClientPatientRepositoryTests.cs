using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Infrastructure.Data;

namespace PalladiumDwh.ClientUploader.Infrastructure.Tests.Data
{
    [TestFixture]
    public class ClientPatientRepositoryTests
    {
        private DwapiRemoteContext _context;
        private ClientFacility _facility;
        private List<ClientPatientExtract> _patients;
        private IClientPatientRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _facility = Builder<ClientFacility>.CreateNew().Build();
            
            _patients = TestHelpers.GetTestPatientWithExtracts(_facility,11, 10).ToList();
            TestHelpers.CreateTestData(_context, _patients);

            _repository=new ClientPatientRepository(_context);
        }

        [Test]
        public void should_Get_All_ClientPatientExtracts()
        {
            var patinets = _repository.GetAll().ToList();

            Assert.IsNotEmpty(patinets);
            var p = patinets.First();
            Assert.IsNotNull(p);


            Assert.IsTrue(p.ClientPatientArtExtracts.Count>0);
            Assert.IsTrue(p.ClientPatientBaselinesExtracts.Count > 0);
            Assert.IsTrue(p.ClientPatientLaboratoryExtracts.Count > 0);
            Assert.IsTrue(p.ClientPatientPharmacyExtracts.Count > 0);
            Assert.IsTrue(p.ClientPatientVisitExtracts.Count > 0);
            Assert.IsTrue(p.ClientPatientStatusExtracts.Count > 0);

        }


        [Test]
        public void should_Get_All_Paged_ClientPatientExtracts()
        {
            var pager = _repository.GetAll(1, 5);

            Assert.IsTrue(pager.TotalItemCount ==11);

            Assert.IsTrue(pager.ToList().Count==5);
            Assert.IsTrue(pager.IsFirstPage);
            

            pager = _repository.GetAll(2, 5);
            Assert.IsTrue(pager.ToList().Count == 5);

            pager = _repository.GetAll(3, 5);
            Assert.IsTrue(pager.ToList().Count == 1);
            Assert.IsTrue(pager.IsLastPage);
            
        }

        [Test]
        public void should_Update_Processed()
        {
            _context=new DwapiRemoteContext();
            _repository = new ClientPatientRepository(_context);
            TestHelpers.CreateTestData(_context, _patients);

            var patient = _repository.GetAll().First();
            Assert.IsNotNull(patient);
            Assert.IsFalse(patient.Processed);

            patient.Processed = true;

            _repository.UpdatePush(patient, "PatientArtExtract",null);

            _context = new DwapiRemoteContext();
            _repository = new ClientPatientRepository(_context);
            var savedPatient = _repository.GetAll().FirstOrDefault(x=>x.Id==patient.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.Processed);

            var artExtraacts = savedPatient.ClientPatientArtExtracts.Where(x => x.Processed == false).ToList();
            Assert.That(artExtraacts.Count,Is.EqualTo(0));

            _context.Database.ExecuteSqlCommand("Delete from PatientExtract;");
        }

        [TearDown]
        public void TearDown()
        {
           
            _context.Dispose();
            _context = null;
        }
    }
}
