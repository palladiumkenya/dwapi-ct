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
    public class SyncIptTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageIptExtract)}", $"{nameof(IptExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<IptSourceBag, IptSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncIpt(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageIptExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageIptExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.IptExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<IptExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<IptSourceBag, IptSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncIpt(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageIptExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageIptExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.IptExtracts.Any());
        }
    }
}
