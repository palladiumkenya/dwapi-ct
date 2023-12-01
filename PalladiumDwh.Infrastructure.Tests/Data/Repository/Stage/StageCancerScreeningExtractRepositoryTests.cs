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
    public class StageCancerScreeningExtractRepositoryTests
    {
        private IStageCancerScreeningExtractRepository _stageCancerScreeningExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageCancerScreeningExtractRepository = TestInitializer.Container.GetInstance<IStageCancerScreeningExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageCancerScreeningExtract)}",$"{nameof(CancerScreeningExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageCancerScreeningExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageCancerScreeningExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageCancerScreeningExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageCancerScreeningExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.CancerScreeningExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageCancerScreeningExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<CancerScreeningExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageCancerScreeningExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageCancerScreeningExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.CancerScreeningExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
