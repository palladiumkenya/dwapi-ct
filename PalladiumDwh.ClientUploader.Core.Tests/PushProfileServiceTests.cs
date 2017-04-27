using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Profile;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Core.Services;
using PalladiumDwh.ClientUploader.Infrastructure.Data;

namespace PalladiumDwh.ClientUploader.Core.Tests
{
    [TestFixture]
    public class PushProfileServiceTests
    {
        private IPushProfileService _service;
        private string _url = "http://data.kenyahmis.org:81/dwapi/api/";
        //private string _url = "http://localhost/dwapi/api/";
        private DwapiRemoteContext _context;
        
        private IClientPatientRepository _clientPatientRepository;
        private ClientPatientExtract _patientExtract;
        private ProfileManager _profileManager;
        private IEnumerable<IClientExtractProfile> _profiles;
        

        [SetUp]
        public void SetUp()
        {
            _patientExtract = TestHelpers.GetTestPatientWithExtracts(1, 5).First();

            _context = new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand("Delete from PatientExtract;");
            _context.ClientPatientExtracts.Add(_patientExtract);
            _context.SaveChanges();

            _clientPatientRepository=new ClientPatientRepository(_context);

            _service =new PushProfileService(_url,_clientPatientRepository);
            
            _profileManager = new ProfileManager();
            _profiles=_profileManager.Generate(_patientExtract);
        }

        [Test]
        public void Should_Push()
        {
            foreach (var extractProfile in _profiles)
            {
                var pushResponse = _service.PushAsync(extractProfile).Result;
                Assert.IsTrue(pushResponse.IsSuccess);

                var patient = _clientPatientRepository
                    .GetAll(true)
                    .FirstOrDefault(x => x.PatientPK == pushResponse.PatientPK && x.SiteCode == pushResponse.SiteCode);

                Assert.IsNotNull(patient);


                if (extractProfile is ClientPatientARTProfile)
                {
                    var extracts = patient.ClientPatientArtExtracts.Where(x => x.Processed == false).ToList();
                    Assert.That(extracts.Count, Is.EqualTo(0));
                }
                if (extractProfile is ClientPatientBaselineProfile)
                {
                    var extracts = patient.ClientPatientBaselinesExtracts.Where(x => x.Processed == false).ToList();
                    Assert.That(extracts.Count, Is.EqualTo(0));

                }
                if (extractProfile is ClientPatientLabProfile)
                {
                    var extracts = patient.ClientPatientLaboratoryExtracts.Where(x => x.Processed == false).ToList();
                    Assert.That(extracts.Count, Is.EqualTo(0));
                }
                if (extractProfile is ClientPatientPharmacyProfile)
                {
                    var extracts = patient.ClientPatientPharmacyExtracts.Where(x => x.Processed == false).ToList();
                    Assert.That(extracts.Count, Is.EqualTo(0));
                }
                if (extractProfile is ClientPatientStatusProfile)
                {
                    var extracts = patient.ClientPatientStatusExtracts.Where(x => x.Processed == false).ToList();
                    Assert.That(extracts.Count, Is.EqualTo(0));
                }
                if (extractProfile is ClientPatientVisitProfile)
                {
                    var extracts = patient.ClientPatientVisitExtracts.Where(x => x.Processed == false).ToList();
                    Assert.That(extracts.Count, Is.EqualTo(0));
                }


                Console.WriteLine($"{extractProfile.GetType().Name} | {pushResponse}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("Delete from PatientExtract;");
        }
    }
}