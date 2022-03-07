using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces.Sync;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Model.Extract;

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
            TestHelpers.ClearDb($"{nameof(StagePatientExtract)}",$"{nameof(PatientExtract)}");
        }
        [Test]
        public void should_Clear_Site()
        {
            _stagePatientExtractRepository.ClearSite(TestInitializer.FacilityId,session).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StagePatientExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var csession = Guid.NewGuid();
            var stages = TestHelpers.CreateTestFacilityStagePatient(TestInitializer.FacilityId,csession);


            _stagePatientExtractRepository.Stage(stages, csession).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.LiveSession==csession));
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var csession = Guid.NewGuid();
            _stagePatientExtractRepository.ClearSite(TestInitializer.FacilityId,session).Wait();
            TestHelpers.CreateTestFacilityPatient(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestFacilityStagePatient(TestInitializer.FacilityId,csession);

            _stagePatientExtractRepository.Stage(stages, csession).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.LiveSession==csession));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.Updated.HasValue));
        }
    }
}
