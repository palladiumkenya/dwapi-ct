using System;
using System.Reflection;
using log4net;
using MediatR;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Manifests.Queries;

namespace PalladiumDwh.Core.Tests.Application.Commands
{
    [TestFixture]
    public class GetValidFacilityTests
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Guid manifestId;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
        }

        [Test]
        public void should_GetValidSite()
        {
            var bag = TestHelpers.GenerateManifest();

            var fac= _mediator.Send(new GetValidFacility(bag)).Result;

            Assert.NotNull(fac);
        }

        [Test]
        public void should_RejectInValidSite()
        {
            var bag = TestHelpers.GenerateManifest(-1);

            var ex=Assert.Throws<AggregateException>(() =>
            {
                var fac = _mediator.Send(new GetValidFacility(bag)).Result;
            });

            Log.Error(ex);
        }

        [Test]
        public void should_RejectInValidFacility()
        {
            var bag = TestHelpers.GenerateManifest(1000);

            var ex=Assert.Throws<AggregateException>(() =>
            {
                var fac = _mediator.Send(new GetValidFacility(bag)).Result;
            });

            Log.Error(ex);
        }
    }
}
