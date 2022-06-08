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
    public class StageAllergiesChronicIllnessExtractRepositoryTests
    {
        private IStageAllergiesChronicIllnessExtractRepository _stageAllergiesChronicIllnessExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageAllergiesChronicIllnessExtractRepository = TestInitializer.Container.GetInstance<IStageAllergiesChronicIllnessExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageAllergiesChronicIllnessExtract)}",$"{nameof(AllergiesChronicIllnessExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageAllergiesChronicIllnessExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageAllergiesChronicIllnessExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageAllergiesChronicIllnessExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageAllergiesChronicIllnessExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageAllergiesChronicIllnessExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageAllergiesChronicIllnessExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.AllergiesChronicIllnessExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageAllergiesChronicIllnessExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<AllergiesChronicIllnessExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageAllergiesChronicIllnessExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageAllergiesChronicIllnessExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageAllergiesChronicIllnessExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageAllergiesChronicIllnessExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.AllergiesChronicIllnessExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
