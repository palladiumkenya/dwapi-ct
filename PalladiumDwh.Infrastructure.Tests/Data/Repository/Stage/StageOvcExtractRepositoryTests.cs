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
    public class StageOvcExtractRepositoryTests
    {
        private IStageOvcExtractRepository _stageOvcExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageOvcExtractRepository = TestInitializer.Container.GetInstance<IStageOvcExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageOvcExtract)}",$"{nameof(OvcExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageOvcExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageOvcExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageOvcExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageOvcExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageOvcExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageOvcExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.OvcExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageOvcExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<OvcExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageOvcExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageOvcExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageOvcExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageOvcExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.OvcExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
