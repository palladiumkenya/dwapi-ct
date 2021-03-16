using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using StructureMap;

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
                .Ctor<string>("queueName").Is(Properties.Settings.Default.QueueName)
                .Ctor<int>("queueBatch").Is(Properties.Settings.Default.QueueBatch)
                .Ctor<int>("profileCount").Is(Properties.Settings.Default.ProfileCount)
                .Ctor<int>("profileBatchCount").Is(Properties.Settings.Default.ProfileBatchCount);

            For<ILiveSyncService>().Use<LiveSyncService>()
                .Ctor<string>("baseUrl").Is(Properties.Settings.Default.LiveSync);

        }

        #endregion
    }
}