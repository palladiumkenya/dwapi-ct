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
    public class StageArtFastTrackExtractRepositoryTests
    {
        private IStageArtFastTrackExtractRepository _stageArtFastTrackExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageArtFastTrackExtractRepository = TestInitializer.Container.GetInstance<IStageArtFastTrackExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageArtFastTrackExtract)}",$"{nameof(ArtFastTrackExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageArtFastTrackExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageArtFastTrackExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageArtFastTrackExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageArtFastTrackExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageArtFastTrackExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageArtFastTrackExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.ArtFastTrackExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageArtFastTrackExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<ArtFastTrackExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageArtFastTrackExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageArtFastTrackExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageArtFastTrackExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageArtFastTrackExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.ArtFastTrackExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
