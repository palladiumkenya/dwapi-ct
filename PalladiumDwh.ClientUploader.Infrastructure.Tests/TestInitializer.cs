using NUnit.Framework;

namespace PalladiumDwh.ClientUploader.Infrastructure.Tests
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