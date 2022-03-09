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
    public class SyncPharmacyTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StagePharmacyExtract)}", $"{nameof(PatientPharmacyExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<PharmacySourceBag, PharmacySourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncPharmacy(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePharmacyExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StagePharmacyExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientPharmacyExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<PatientPharmacyExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<PharmacySourceBag, PharmacySourceDto>(TestInitializer.FacilityId,
                currentManifestId);
            _mediator.Send(new SyncPharmacy(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePharmacyExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StagePharmacyExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.PatientPharmacyExtracts.Any());
        }
    }
}
