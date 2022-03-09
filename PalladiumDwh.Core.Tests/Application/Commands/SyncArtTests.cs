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
    public class SyncArtTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageArtExtract)}", $"{nameof(PatientArtExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<ArtSourceBag, ArtSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncArt(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageArtExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageArtExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientArtExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<PatientArtExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<ArtSourceBag, ArtSourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncArt(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageArtExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageArtExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientArtExtracts.Any());
        }
    }
}
