using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces.Sync;
using PalladiumDwh.Infrastructure.Data;

namespace PalladiumDwh.Infrastructure.Tests.Data.Repository.Sync
{
    [TestFixture]
    public class StagePatientExtractRepositoryTests
    {
        private IStagePatientExtractRepository _stagePatientExtractRepository;
        private Guid session;

        [SetUp]
        public void SetUp()
        {
            session = Guid.NewGuid();
            _stagePatientExtractRepository = TestInitializer.Container.GetInstance<IStagePatientExtractRepository>();
            TestHelpers.CreateTestFacilityStage(TestInitializer.FacilityId);
        }
        [Test]
        public void should_Clear_Site()
        {
            _stagePatientExtractRepository.ClearSite(TestInitializer.FacilityId,session).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StagePatientExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage()
        {
            var stages = TestHelpers.CreateTestFacilityStagePatient(TestInitializer.FacilityId);
            var csession = stages.First().LiveSession.Value;

            _stagePatientExtractRepository.Stage(stages, csession).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.LiveSession==csession));
        }
    }
}
