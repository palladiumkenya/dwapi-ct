using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.MapProfiles;
using PalladiumDwh.Infrastructure.Data;
using StructureMap;
using StructureMap.Pipeline;

namespace PalladiumDwh.Core.Tests.DependencyResolution {
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
                   scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                   scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
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

            For<IMediator>().LifecycleIs<TransientLifecycle>().Use<Mediator>();
            For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
        }
        #endregion
    }
}
