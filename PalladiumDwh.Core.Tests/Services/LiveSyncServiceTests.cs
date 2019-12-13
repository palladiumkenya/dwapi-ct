using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class LiveSyncServiceTests
    {
        private ILiveSyncService _liveSyncService;
        private List<ManifestDto> _manifestDtos = new List<ManifestDto>();
        private List<MetricDto> _metricDtos = new List<MetricDto>();
        private readonly Guid fmId = Guid.NewGuid();


        [SetUp]
        public void SetUp()
        {
            _liveSyncService = new LiveSyncService("http://localhost:4777/stages/");
            _manifestDtos.Add(new ManifestDto(10000, "Demo Fac 00", new FacilityManifest(10000, 120)));
            _manifestDtos.Add(new ManifestDto(10001, "Demo Fac 01", new FacilityManifest(10001, 1120)));

            _metricDtos.Add(new MetricDto(10001, "Demo Fac 01",
                new FacilityManifestCargo("{\"EmrName\":\"Demo EMR\",\"EmrVersion\":\"v1.0.0.0\",\"LastLoginDate\":\"1983-07-04T00:00:00\",\"LastMoH731RunDate\":\"1983-07-04T00:00:00\",\"DateExtracted\":\"2019-12-13T16:12:21.159234\",\"Id\":\"ff22cd7c-083e-4d6d-b800-ab2200d9a053\"}", fmId, CargoType.AppMetrics)));
            _metricDtos.Add(new MetricDto(10001, "Demo Fac 01",
                new FacilityManifestCargo("{\"Version\":\"2.3.9\",\"Name\":\"MasterPatientIndex\",\"LogDate\":\"2019-12-13T12:04:07.536592\",\"LogValue\":\"{\\\"Name\\\":\\\"MasterPatientIndex\\\",\\\"NoLoaded\\\":0,\\\"Version\\\":\\\"2.3.9\\\",\\\"ActionDate\\\":\\\"2019-12-13T12:04:07.535936+03:00\\\"}\",\"Status\":0,\"Display\":\"Master Patient Index\",\"Action\":\"Loaded\",\"Rank\":5,\"Id\":\"b9ae5ec8-bc7f-411e-9ab0-ab22009572d5\"}", fmId, CargoType.Metrics)));
        }

        [Test]
        public void should_Sync_Manifest()
        {
            _manifestDtos.ForEach(m =>
            {
                var result = _liveSyncService.SyncManifest(m).Result;
                Assert.True(result.IsSuccess);
            });
        }

        [Test]
        public void should_Sync_Stats()
        {
            var facIds = _manifestDtos.Select(x => x.Id).ToList();
            var facStats = new List<StatsDto>();
            int fcode = 10000;
            int pCount = 120;

            facIds.ToList().ForEach(f =>
            {
                var stats = new StatsDto(fcode, DateTime.Now);
                stats.AddStats("PatientExtract", pCount - (pCount * 50) / 100);
                stats.AddStats("PatientAdverseEventExtract", pCount - (pCount * 90) / 100);
                stats.AddStats("PatientArtExtract", pCount - (pCount * 50) / 100);
                stats.AddStats("PatientBaselineExtract", pCount - (pCount * 50) / 100);
                stats.AddStats("PatientLabExtract", pCount * 34);
                stats.AddStats("PatientPharmacyExtract", pCount * 25);
                stats.AddStats("PatientStatusExtract", pCount - (pCount * 50) / 100);
                stats.AddStats("PatientVisitExtract", pCount * 30);
                facStats.Add(stats);
                fcode++;
                pCount = pCount * 10;
            });

            var mock = new Mock<IFacilityRepository>();
            mock.Setup(s => s.GetFacStats(facIds)).Returns(facStats);

            var result = _liveSyncService.SyncStats(mock.Object, facIds).Result;
            Assert.True(result.IsSuccess);
        }

        [Test]
        public void should_Sync_Metrics()
        {
            var result = _liveSyncService.SyncMetrics(_metricDtos).Result;
            Assert.True(result.IsSuccess);

        }
    }
}
