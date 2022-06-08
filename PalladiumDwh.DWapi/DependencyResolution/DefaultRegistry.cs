using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.MapProfiles;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.DWapi.Helpers;
using PalladiumDwh.Infrastructure.Data;
using StructureMap;
using StructureMap.Pipeline;

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
                    scan.Assembly("PalladiumDwh.Shared.Data");
                    scan.Assembly("PalladiumDwh.Core");
                    scan.Assembly("PalladiumDwh.Infrastructure");
                    scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            For<DwapiCentralContext>().Use<DwapiCentralContext>()
              .SelectConstructor(() => new DwapiCentralContext());

            For<IMessagingSenderService>().Use<MessagingSenderService>()
                .Ctor<string>("queueName").Is(Properties.Settings.Default.QueueName);

            For<ILiveSyncService>().Use<LiveSyncService>()
                .Ctor<string>("baseUrl").Is(Properties.Settings.Default.LiveSync);

            For<IMessengerScheduler>().Use<MessengerScheduler>().Singleton();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CtProfile());
            });

            var mapper = config.CreateMapper();
            For<IMapper>().Use(mapper);

            For<IMediator>().LifecycleIs<TransientLifecycle>().Use<Mediator>();
            For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
        }
    }
}
