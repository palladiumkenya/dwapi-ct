using System;
using NUnit.Framework;
using PalladiumDwh.Infrastructure.Tests.DependencyResolution;
using StructureMap;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static Guid FacilityId;
        public static IContainer Container;

        [OneTimeSetUp]
        public void Init()
        {
            FacilityId = Guid.NewGuid();
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            DapperPlusManager.AddLicense("1755;701-ThePalladiumGroup", "9005d618-3965-8877-97a5-7a27cbb21c8f");
            Container = IoC.Initialize();
            InitSites(FacilityId);
        }

        private void InitSites(Guid facilityId)
        {
            TestHelpers.CreateTestMasterFacility();
            TestHelpers.CreateTestFacility(facilityId);
        }
    }
}
