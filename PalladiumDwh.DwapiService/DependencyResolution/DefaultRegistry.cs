using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using StructureMap;
using StructureMap.Pipeline;

namespace PalladiumDWh.DwapiService.DependencyResolution {
    public class DefaultRegistry : Registry {

        
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
               scan =>
               {
                   scan.TheCallingAssembly();
                   scan.Assembly("PalladiumDwh.Shared");
                   scan.Assembly("PalladiumDwh.Core");
                   scan.Assembly("PalladiumDwh.Infrastructure");
                   scan.WithDefaultConventions();
               });

            For<DwapiCentralContext>().Use<DwapiCentralContext>()
                .SelectConstructor(() => new DwapiCentralContext());


            For<IMessagingReaderService>().Use<MessagingReaderService>()
                .Ctor<string>("queueName").Is(PalladiumDWh.DwapiService.Properties.Settings.Default.QueueName)
                .Ctor<int>("queueBatch").Is(PalladiumDWh.DwapiService.Properties.Settings.Default.QueueBatch);
            

        }

        #endregion
    }
}