using AutoMapper;
using NUnit.Framework;
using PalladiumDwh.Core.Model.Profiles;
using PalladiumDwh.Core.Tests.DependencyResolution;
using StructureMap;

namespace PalladiumDwh.Core.Tests
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IContainer Container;
        [OneTimeSetUp]
        public void Init()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Container = IoC.Initialize();
        }
    }
}
