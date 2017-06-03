using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Tests
{
    public static class TestHelpers
    {
        public static Manifest GetTestManifest()
        {
            var mainfest = Builder<Manifest>.CreateNew().Build();
            mainfest.SiteCode = 15311;
            mainfest.PatientPKs.AddRange(new List<int> {1, 2, 3, 4, 5});
            return mainfest;
        }

        public static IEnumerable<ClientPatientExtract> GetTestPatientWithExtracts(int patientCount, int count)
        {
            var patients = Builder<ClientPatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.Id = LiveGuid.NewGuid();
                p.Processed = false;
                p.AddPatientArtExtracts(Builder<ClientPatientArtExtract>.CreateListOfSize(1).All().With(x=>x.Processed=false).Build().ToList());
                p.AddPatientBaselinesExtracts(Builder<ClientPatientBaselinesExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientLaboratoryExtracts(Builder<ClientPatientLaboratoryExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientPharmacyExtracts(Builder<ClientPatientPharmacyExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientStatusExtracts(Builder<ClientPatientStatusExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientVisitExtracts(Builder<ClientPatientVisitExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
            }
            return patients;
        }
        public static IEnumerable<ClientPatientExtract> GetTestPatientWithExtracts(int patientCount, int count,int patientId, int siteCode)
        {
            var patients = Builder<ClientPatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.Id = LiveGuid.NewGuid();
                p.PatientPK = patientId;
                p.SiteCode = siteCode;
                p.Processed = false;
                p.AddPatientArtExtracts(Builder<ClientPatientArtExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientBaselinesExtracts(Builder<ClientPatientBaselinesExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientLaboratoryExtracts(Builder<ClientPatientLaboratoryExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientPharmacyExtracts(Builder<ClientPatientPharmacyExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientStatusExtracts(Builder<ClientPatientStatusExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientVisitExtracts(Builder<ClientPatientVisitExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
            }
            return patients;
        }

        public static IEnumerable<SiteProfile> GetSiteProfiles()
        {
            var importPath = $@"{TestContext.CurrentContext.TestDirectory.HasToEndsWith(@"\")}Imports";
            var siteManifestJson = $@"{importPath.HasToEndsWith(@"\")}SiteManifestA.json";

            var fileContent = File.ReadAllText(siteManifestJson);
            Assert.IsTrue(fileContent.Length > 0);
            var siteManifest = JsonConvert.DeserializeObject<SiteManifest>(fileContent);
            Assert.IsNotNull(siteManifest);

            var siteProfiles = new ProfileManager().GenerateSiteProfiles(siteManifest).ToList();

            return siteProfiles;
        }
    }
}