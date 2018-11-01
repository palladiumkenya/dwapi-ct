using NUnit.Framework;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        [OneTimeSetUp]
        public void Init()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            DapperPlusManager.AddLicense("1755;701-ThePalladiumGroup", "9005d618-3965-8877-97a5-7a27cbb21c8f");
        }

    }
}