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
    public class SyncCancerScreeningTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageCancerScreeningExtract)}", $"{nameof(CancerScreeningExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<CancerScreeningSourceBag, CancerScreeningSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncCancerScreening(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.CancerScreeningExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<CancerScreeningExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<CancerScreeningSourceBag, CancerScreeningSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncCancerScreening(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageCancerScreeningExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.CancerScreeningExtracts.Any());
        }
    }
}
