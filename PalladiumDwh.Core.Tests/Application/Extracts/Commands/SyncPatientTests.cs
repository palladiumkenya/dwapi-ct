using System;
using System.Linq;
using MediatR;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Extracts.Commands;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Tests.Application.Extracts.Commands
{
    [TestFixture]
    public class SyncPatientTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StagePatientExtract)}",$"{nameof(PatientExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GeneratePatientBag(TestInitializer.FacilityId, currentManifestId);

            _mediator.Send(new SyncPatient(bag)).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.False(ctx.PatientExtracts.ToList().Any(x=>x.Updated.HasValue));
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreatePatientExtracts(TestInitializer.FacilityId);
            var bag = TestHelpers.GeneratePatientBag(TestInitializer.FacilityId, currentManifestId);

            _mediator.Send(new SyncPatient(bag)).Wait();

            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.FacilityId==TestInitializer.FacilityId));
            Assert.True(ctx.StagePatientExtracts.ToList().Any(x=>x.LiveSession==currentManifestId));
            Assert.True(ctx.PatientExtracts.ToList().Any(x=>x.Updated.HasValue));
        }
    }
}
