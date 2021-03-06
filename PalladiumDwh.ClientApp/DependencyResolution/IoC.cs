using System.Reflection;
using System.Threading.Tasks;
using log4net;
using StructureMap;

namespace PalladiumDwh.ClientApp.DependencyResolution
{
    public static class IoC
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IContainer Initialize()
        {
            var container = new Container(c => c.AddRegistry<DefaultRegistry>());
            if (Properties.Settings.Default.devMode)
                Log.Debug(container.WhatDoIHave());
            return container;
        }
        public static Task<IContainer> InitializeAsync()
        {
            return Task.Run(() => Initialize());
        }
    }
}