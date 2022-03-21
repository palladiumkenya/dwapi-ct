using System;
using System.Reflection;
using log4net;
using MediatR;
using NUnit.Framework;
using PalladiumDwh.Core.Application.Commands;

namespace PalladiumDwh.Core.Tests.Application.Manifests.Commands
{
    [TestFixture]
    public class ClearManifestTests
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
        public void should_Execute()
        {
            var bag = TestHelpers.GenerateManifest();

            var fac= _mediator.Send(new ClearManifest(bag)).Result;

            Assert.True(fac.IsSuccess);
        }
    }
}
