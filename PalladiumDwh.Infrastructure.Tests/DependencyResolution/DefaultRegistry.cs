using AutoMapper;
using PalladiumDwh.Core.Application.MapProfiles;
using PalladiumDwh.Infrastructure.Data;
using StructureMap;

namespace PalladiumDwh.Infrastructure.Tests.DependencyResolution {
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
                   scan.Assembly("PalladiumDwh.Shared.Data");
                   scan.WithDefaultConventions();
               });

            For<DwapiCentralContext>().Use<DwapiCentralContext>()
                .SelectConstructor(() => new DwapiCentralContext());

            /*
            For<ILiveSyncService>().Use<LiveSyncService>()
                .Ctor<string>("baseUrl").Is(Properties.Settings.Default.LiveSync);
            */

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CtProfile());
            });

            var mapper = config.CreateMapper();
            For<IMapper>().Use(mapper);
        }
        #endregion
    }
}
