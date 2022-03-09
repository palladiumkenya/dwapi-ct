using System;
using AutoMapper;
using NUnit.Framework;
using PalladiumDwh.Core.Model.Profiles;
using PalladiumDwh.Core.Tests.DependencyResolution;
using StructureMap;
using Z.Dapper.Plus;

namespace PalladiumDwh.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static Guid FacilityId,FacilityId2;
        public static IContainer Container;

        [OneTimeSetUp]
        public void Init()
        {
            FacilityId = new Guid("C3F718E7-CCC1-471F-9A87-33B96CBFE519");
            FacilityId2 = new Guid("C4F448E4-CCC1-471F-9A87-33B96CBFE519");
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            DapperPlusManager.AddLicense("1755;700-ThePalladiumGroup", "218460a6-02d0-c26b-9add-e6b8d13ccbf4");
            Container = IoC.Initialize();
            InitSites(FacilityId,FacilityId2);
        }

        private void InitSites(Guid facilityId,Guid facilityId2)
        {
            TestHelpers.CreateDb();
            TestHelpers.ClearDb();
            TestHelpers.CreateTestMasterFacility();
            TestHelpers.CreateTestMasterFacility(99991,"National Test (99991)");
            TestHelpers.CreateTestFacility(facilityId);
            TestHelpers.CreateTestFacility(facilityId2,99991,"National Test (99991)");
        }
    }
}
