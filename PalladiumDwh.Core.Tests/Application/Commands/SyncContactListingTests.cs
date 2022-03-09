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
    public class SyncContactListingTests
    {
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
            manifestId = Guid.NewGuid();
            TestHelpers.ClearDb($"{nameof(StageContactListingExtract)}", $"{nameof(ContactListingExtract)}");
        }

        [Test]
        public void should_Sync_New()
        {
            var currentManifestId = Guid.NewGuid();
            var bag = TestHelpers.GenerateBag<ContactListingSourceBag, ContactListingSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncContactListing(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageContactListingExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageContactListingExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.ContactListingExtracts.Any());
        }

        [Test]
        public void should_Sync_Updates()
        {
            var currentManifestId = Guid.NewGuid();
            TestHelpers.CreateTestExtract<ContactListingExtract>(TestInitializer.FacilityId);
            var bag = TestHelpers.GenerateBag<ContactListingSourceBag, ContactListingSourceDto>(
                TestInitializer.FacilityId, currentManifestId);
            _mediator.Send(new SyncContactListing(bag)).Wait();
            var ctx = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            Assert.True(ctx.StageContactListingExtracts.ToList().Any(x => x.FacilityId == TestInitializer.FacilityId));
            Assert.True(ctx.StageContactListingExtracts.ToList().Any(x => x.LiveSession == currentManifestId));
            Assert.True(ctx.ContactListingExtracts.Any());
        }
    }
}
