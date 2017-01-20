using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using StructureMap;

namespace PalladiumDwh.DWapi.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.Assembly("PalladiumDwh.Shared");
                    scan.Assembly("PalladiumDwh.Core");
                    scan.Assembly("PalladiumDwh.Infrastructure");
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            For<DwapiCentralContext>().Use<DwapiCentralContext>()
              .SelectConstructor(() => new DwapiCentralContext());

            For<IMessagingSenderService>().Use<MessagingSenderService>()
                .Ctor<string>("queueName").Is(Properties.Settings.Default.QueueName);              
        }
    }
}