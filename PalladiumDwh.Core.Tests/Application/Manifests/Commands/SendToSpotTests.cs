using System;
using System.Reflection;
using log4net;
using MediatR;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Commands;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Tests.Application.Manifests.Commands
{
    [TestFixture]
    public class SendToSpotTests
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Guid manifestId;
        private IMediator _mediator;
        private MasterFacility _masterFacility;

        [SetUp]
        public void SetUp()
        {
            _masterFacility = new MasterFacility() {Code = 99990, Name = "Maun Hospital (99990)"};
            _mediator = TestInitializer.Container.GetInstance<IMediator>();
        }

        [Test]
        public void should_Execute()
        {
            var bag = TestHelpers.GenerateManifest();

            var fac= _mediator.Send(new SendToSpot(bag,_masterFacility)).Result;

            Assert.True(fac.IsSuccess);
        }
    }
}
