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
    public class StageBaselineExtractRepositoryTests
    {
        private IStageBaselineExtractRepository _stageBaselineExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageBaselineExtractRepository = TestInitializer.Container.GetInstance<IStageBaselineExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageBaselineExtract)}",$"{nameof(PatientBaselinesExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageBaselineExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageBaselineExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageBaselineExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageBaselineExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.PatientBaselinesExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageBaselineExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<PatientBaselinesExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageBaselineExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageBaselineExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientBaselinesExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
