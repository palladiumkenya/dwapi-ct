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
    public class SyncAdverseEventTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageAdverseEventExtract)}", $"{nameof(PatientAdverseEventExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<AdverseEventSourceBag, AdverseEventSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncAdverseEvent(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageAdverseEventExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageAdverseEventExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientAdverseEventExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<PatientAdverseEventExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<AdverseEventSourceBag, AdverseEventSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncAdverseEvent(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageAdverseEventExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageAdverseEventExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientAdverseEventExtracts.Any());
        }
    }
}
