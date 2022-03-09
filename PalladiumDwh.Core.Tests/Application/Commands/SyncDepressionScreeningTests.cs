using System;
using System.Linq;
using MediatR;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Commands;
using PalladiumDwh.Core.Application.Source;
using PalladiumDwh.Core.Application.Source.Dto;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Tests.Application.Commands
{
    [TestFixture]
    public class SyncDepressionScreeningTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageDepressionScreeningExtract)}", $"{nameof(DepressionScreeningExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<DepressionScreeningSourceBag, DepressionScreeningSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncDepressionScreening(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageDepressionScreeningExtracts.ToList()
                .Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageDepressionScreeningExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.DepressionScreeningExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<DepressionScreeningExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<DepressionScreeningSourceBag, DepressionScreeningSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncDepressionScreening(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageDepressionScreeningExtracts.ToList()
                .Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageDepressionScreeningExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.DepressionScreeningExtracts.Any());
        }
    }
}
