using NUnit.Framework;

namespace PalladiumDwh.DWapi.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        [OneTimeSetUp]
        public void Init()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }

    }
}