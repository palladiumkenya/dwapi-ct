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
            FacilityId = new Guid("C2F718E7-CCC1-471F-9A87-33B96CBFE519");
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "218460a6-02d0-c26b-9add-e6b8d13ccbf4");
            Container = IoC.Initialize();
            InitSites(FacilityId);
        }

        private void InitSites(Guid facilityId)
        {
            TestHelpers.CreateDb();
            TestHelpers.ClearDb();
            TestHelpers.CreateTestMasterFacility();
            TestHelpers.CreateTestFacility(facilityId);
        }
    }
}
