using NUnit.Framework;

namespace PalladiumDwh.Infrastructure.Tests
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