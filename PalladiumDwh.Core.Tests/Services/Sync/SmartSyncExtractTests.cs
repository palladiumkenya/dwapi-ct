using System.Reflection;
using log4net;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces.Sync;

namespace PalladiumDwh.Core.Tests.Services.Sync
{
    [TestFixture]
    public class SmartSyncExtractTests
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ISyncExtract _sync;

        [SetUp]
        public void SetUp()
        {
            _sync = TestInitializer.Container.GetInstance<ISyncExtract>();
        }

        [Test]
        public void should_ProcessPrimary()
        {

        }
    }
}
