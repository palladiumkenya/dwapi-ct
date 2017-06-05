using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Profile;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Core.Services;
using PalladiumDwh.ClientUploader.Infrastructure.Data;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Tests
{
    [TestFixture]
    public class PushProfileServiceTests
    {
        private IPushProfileService _service;
        //private string _url = "http://data.kenyahmis.org:81/dwapi/api/";
        private string _url = "http://localhost:88/dwapi/api/";
        private DwapiRemoteContext _context;
        
        private IClientPatientRepository _clientPatientRepository;
        private ClientPatientExtract _patientExtract;
        private ProfileManager _profileManager;
        private IEnumerable<IClientExtractProfile> _profiles;
        private ClientPatientExtract _patientExtract2;
        private IEnumerable<IClientExtractProfile> _profiles2;
        private Manifest _manifest;
        private IProgress<DProgress> _progress;
        private string _importPath;
        private string _siteManifestJson;

        [SetUp]
        public void SetUp()
        {
            _patientExtract = TestHelpers.GetTestPatientWithExtracts(1, 5).First();
            _patientExtract2 = TestHelpers.GetTestPatientWithExtracts(1, 5, 0, 0).First();

            _context = new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand("Delete from PatientExtract;");
            _context.ClientPatientExtracts.Add(_patientExtract);
            _context.SaveChanges();

            _clientPatientRepository=new ClientPatientRepository(_context);

            _service =new PushProfileService(_url,_clientPatientRepository);
            
            _profileManager = new ProfileManager();
            _profiles=_profileManager.Generate(_patientExtract);
            _profiles2 = _profileManager.Generate(_patientExtract2);

            _manifest = TestHelpers.GetTestManifest();
            _progress = new Progress<DProgress>(ReportProgress);

        }



        [Test]
        public void Should_Spot()
        {      
            var response = _service.SpotAsync(_manifest,_progress).Result;
            Assert.IsTrue(response.Length > 0);
            Console.WriteLine(response);
        }

        [Test]
        public void Should_Spot_Gzipped()
        {
            for (int j = 0; j < 50000; j++)
            {
                _manifest.PatientPKs.Add(j);
            }
            string man = JsonConvert.SerializeObject(_manifest);
            var response = _service.SpotAsync(_manifest, _progress).Result;
            Assert.IsTrue(response.Length > 0);
            Console.WriteLine(response);
        }
        [Test]
        public void Should_Handle_Spot_Error()
        {
            _manifest.SiteCode = -1;
            var ex = Assert.Throws<AggregateException>(() =>
                {
                    var response= _service.SpotAsync(_manifest, _progress).Result;
                }
            );
            Console.WriteLine(ex.InnerException.Message);
}
        [Test]
        public void Should_Handle_Spot_Error_Invalid_MFL()
        {
            _manifest.SiteCode = 10;
            var ex = Assert.Throws<AggregateException>(() =>
                {
                    var response = _service.SpotAsync(_manifest, _progress).Result;
                }
            );
            Console.WriteLine(ex.InnerException.Message);
        }

        [Test]
        public void Should_Push_SiteProfile()
        {
            var siteProfile = TestHelpers.GetSiteProfiles().First();
            var pushResponse = _service.PushAsync(siteProfile).Result;
            Assert.IsTrue(pushResponse.IsSuccess);
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

        [Test]
        public void Should_Handle_Push_Network_Errors()
        {
            var extractProfile = _profiles.FirstOrDefault(x => x.Source == "PatientArtExtract");

            //Network Error
            _service = new PushProfileService("http://localhostt/dwapi/api/", _clientPatientRepository);
            var pushResponse = _service.PushAsync(extractProfile).Result;
            Assert.IsFalse(pushResponse.IsSuccess);

            _context = new DwapiRemoteContext();
            var extracts = _context.ClientPatientArtExtracts.Where(x => x.Status != "Sent").ToList();
            Assert.IsTrue(extracts.Count > 0);
            Console.WriteLine($"{extracts.Count} Failed to send");
            foreach (var e in extracts)
            {
                Console.WriteLine($"{e} | {e.Status}");
            }

            Console.WriteLine(pushResponse.Status);
            Console.WriteLine($"{extractProfile.GetType().Name} | {pushResponse}");
        }
        [Test]
        public void Should_Handle_Push_Send_Errors()
        {
            var extractProfile = _profiles2.FirstOrDefault(x => x.Source == "PatientArtExtract");

            //Send Error
            _service = new PushProfileService("http://localhost/dwapi/api/", _clientPatientRepository);
            var pushResponse = _service.PushAsync(extractProfile).Result;
            Assert.IsFalse(pushResponse.IsSuccess);

            _context = new DwapiRemoteContext();
            var extracts = _context.ClientPatientArtExtracts.Where(x => x.Status!="Sent").ToList();
            Assert.IsTrue(extracts.Count > 0);
            Console.WriteLine($"{extracts.Count} Failed to send");
            foreach (var e in extracts)
            {
                Console.WriteLine($"{e} | {e.Status}");
            }

            Console.WriteLine(pushResponse.Status);
            Console.WriteLine($"{extractProfile.GetType().Name} | {pushResponse}");
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("Delete from PatientExtract;");
        }

        private void ReportProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}