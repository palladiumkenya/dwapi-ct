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
    public class StageDrugAlcoholScreeningExtractRepositoryTests
    {
        private IStageDrugAlcoholScreeningExtractRepository _stageDrugAlcoholScreeningExtractRepository;
        private Guid manifestId;

        [SetUp]
        public void SetUp()
        {
            manifestId = Guid.NewGuid();
            _stageDrugAlcoholScreeningExtractRepository = TestInitializer.Container.GetInstance<IStageDrugAlcoholScreeningExtractRepository>();
            TestHelpers.ClearDb($"{nameof(StageDrugAlcoholScreeningExtract)}",$"{nameof(DrugAlcoholScreeningExtract)}",$"{nameof(PatientExtract)}",$"{nameof(ActionRegister)}");
        }

        [Test]
        public void should_Clear_Site()
        {
            _stageDrugAlcoholScreeningExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.False(ctx.StageDrugAlcoholScreeningExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_New()
        {
            var currentManifestId = Guid.NewGuid();
            var stages = TestHelpers.CreateTestStage<StageDrugAlcoholScreeningExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageDrugAlcoholScreeningExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageDrugAlcoholScreeningExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageDrugAlcoholScreeningExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.DrugAlcoholScreeningExtracts.ToList().Any());
        }

        [Test]
        public void should_Stage_Mixed()
        {
            var currentManifestId = Guid.NewGuid();
            _stageDrugAlcoholScreeningExtractRepository.ClearSite(TestInitializer.FacilityId,manifestId).Wait();
            TestHelpers.CreateTestExtract<DrugAlcoholScreeningExtract>(TestInitializer.FacilityId);
            var stages = TestHelpers.CreateTestStage<StageDrugAlcoholScreeningExtract>(TestInitializer.FacilityId,currentManifestId);

            _stageDrugAlcoholScreeningExtractRepository.SyncStage(stages, currentManifestId).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageDrugAlcoholScreeningExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageDrugAlcoholScreeningExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.DrugAlcoholScreeningExtracts.ToList().Any(x=>x.Created.HasValue));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
        }
    }
}
