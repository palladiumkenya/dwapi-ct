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
    public class StageEnhancedAdherenceCounsellingExtractRepositoryTests
    {
        private IStageEnhancedAdherenceCounsellingExtractRepository _stageEnhancedAdherenceCounsellingExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageEnhancedAdherenceCounsellingExtractRepository = TestInitializer.Container.GetInstance<IStageEnhancedAdherenceCounsellingExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageEnhancedAdherenceCounsellingExtract)}",$"{nameof(EnhancedAdherenceCounsellingExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageEnhancedAdherenceCounsellingExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageEnhancedAdherenceCounsellingExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageEnhancedAdherenceCounsellingExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageEnhancedAdherenceCounsellingExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageEnhancedAdherenceCounsellingExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageEnhancedAdherenceCounsellingExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.EnhancedAdherenceCounsellingExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageEnhancedAdherenceCounsellingExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<EnhancedAdherenceCounsellingExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageEnhancedAdherenceCounsellingExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageEnhancedAdherenceCounsellingExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageEnhancedAdherenceCounsellingExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageEnhancedAdherenceCounsellingExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.EnhancedAdherenceCounsellingExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
