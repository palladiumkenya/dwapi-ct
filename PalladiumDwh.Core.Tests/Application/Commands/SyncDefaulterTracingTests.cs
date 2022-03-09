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
    public class SyncDefaulterTracingTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageDefaulterTracingExtract)}", $"{nameof(DefaulterTracingExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<DefaulterTracingSourceBag, DefaulterTracingSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncDefaulterTracing(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageDefaulterTracingExtracts.ToList()
                .Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageDefaulterTracingExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.DefaulterTracingExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<DefaulterTracingExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<DefaulterTracingSourceBag, DefaulterTracingSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncDefaulterTracing(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageDefaulterTracingExtracts.ToList()
                .Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageDefaulterTracingExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.DefaulterTracingExtracts.Any());
        }
    }
}
