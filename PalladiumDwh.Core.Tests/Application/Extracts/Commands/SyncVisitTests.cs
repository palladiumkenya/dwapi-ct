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
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Tests.Application.Commands
{
    [TestFixture]
    public class SyncVisitTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageVisitExtract)}",$"{nameof(PatientVisitExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<VisitSourceBag,VisitSourceDto>(TestInitializer.FacilityId, currentManifestId);

            _mediator.Send(new SyncVisit(bag)).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageVisitExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageVisitExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientVisitExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<PatientVisitExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<VisitSourceBag,VisitSourceDto>(TestInitializer.FacilityId, currentManifestId);

            _mediator.Send(new SyncVisit(bag)).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageVisitExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StageVisitExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientVisitExtracts.Any());
        }
    }
}
