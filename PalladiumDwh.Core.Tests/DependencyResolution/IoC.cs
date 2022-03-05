using System.Reflection;
using log4net;
using StructureMap;

namespace PalladiumDwh.Core.Tests.DependencyResolution {
    public static class IoC {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static IContainer Initialize() {
            var container= new Container(c => c.AddRegistry<DefaultRegistry>());
            Log.Debug(container.WhatDoIHave());
            return container;
        }
    }
}
