using System;
using System.Linq;
using MediatR;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Extracts.Commands;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Tests.Application.Extracts.Commands
{
    [TestFixture]
    public class SyncBaselineTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageBaselineExtract)}", $"{nameof(PatientBaselinesExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<BaselineSourceBag, BaselineSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncBaseline(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientBaselinesExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<PatientBaselinesExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<BaselineSourceBag, BaselineSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncBaseline(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageBaselineExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientBaselinesExtracts.Any());
        }
    }
}
