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
    public class StageCovidExtractRepositoryTests
    {
        private IStageCovidExtractRepository _stageCovidExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageCovidExtractRepository = TestInitializer.Container.GetInstance<IStageCovidExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageCovidExtract)}",$"{nameof(CovidExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageCovidExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageCovidExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageCovidExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageCovidExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCovidExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageCovidExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.CovidExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageCovidExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<CovidExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageCovidExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageCovidExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCovidExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageCovidExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.CovidExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
