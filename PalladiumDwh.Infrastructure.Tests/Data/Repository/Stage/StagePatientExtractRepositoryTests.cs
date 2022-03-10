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
    public class StagePatientExtractRepositoryTests
    {
        private IStagePatientExtractRepository _stagePatientExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stagePatientExtractRepository = TestInitializer.Container.GetInstance<IStagePatientExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StagePatientExtract)}",$"{nameof(PatientExtract)}");
        }
        [Test]
        public void should_Clear_Site()
        {
            _stagePatientExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StagePatientExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestFacilityStagePatient(TestInitializer.FacilityId,currentManifestId);


            _stagePatientExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stagePatientExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestFacilityPatient(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestFacilityStagePatient(TestInitializer.FacilityId,currentManifestId);

            _stagePatientExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.Updated.HasValue));
        }
    }
}
