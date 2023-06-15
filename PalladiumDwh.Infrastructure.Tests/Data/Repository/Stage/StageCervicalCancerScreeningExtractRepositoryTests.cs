using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Tests.Data.Repository.Stage
{
    [TestFixture]
    public class StageCervicalCancerScreeningExtractRepositoryTests
    {
        private IStageCervicalCancerScreeningExtractRepository _stageCervicalCancerScreeningExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageCervicalCancerScreeningExtractRepository = TestInitializer.Container.GetInstance<IStageCervicalCancerScreeningExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageCervicalCancerScreeningExtract)}",$"{nameof(CervicalCancerScreeningExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageCervicalCancerScreeningExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageCervicalCancerScreeningExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageCervicalCancerScreeningExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageCervicalCancerScreeningExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCervicalCancerScreeningExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageCervicalCancerScreeningExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.CervicalCancerScreeningExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageCervicalCancerScreeningExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<CervicalCancerScreeningExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageCervicalCancerScreeningExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageCervicalCancerScreeningExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCervicalCancerScreeningExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageCervicalCancerScreeningExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.CervicalCancerScreeningExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
