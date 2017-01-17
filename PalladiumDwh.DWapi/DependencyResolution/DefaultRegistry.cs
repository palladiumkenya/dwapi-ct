using System.Reflection;
using log4net;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using PalladiumDwh.Infrastructure.Data;


namespace PalladiumDwh.DWapi.DependencyResolution {

    public class DefaultRegistry : Registry {


        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.Assembly("PalladiumDwh.Shared");
                    scan.Assembly("PalladiumDwh.Core");
                    scan.Assembly("PalladiumDwh.Infrastructure");
                    scan.WithDefaultConventions();
                });
            
            For<DwapiCentralContext>().Use<DwapiCentralContext>()
                .SelectConstructor(() => new DwapiCentralContext());
        }
    }
}