using System;
using System.Linq;
using MediatR;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Commands;
using PalladiumDwh.Core.Application.Extracts.Commands;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Tests;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Tests.Application.Commands
{
    [TestFixture]
    public class SyncGbvScreeningTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageGbvScreeningExtract)}", $"{nameof(GbvScreeningExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<GbvScreeningSourceBag, GbvScreeningSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncGbvScreening(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageGbvScreeningExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageGbvScreeningExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.GbvScreeningExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<GbvScreeningExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<GbvScreeningSourceBag, GbvScreeningSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncGbvScreening(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageGbvScreeningExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageGbvScreeningExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.GbvScreeningExtracts.Any());
        }
    }
}
