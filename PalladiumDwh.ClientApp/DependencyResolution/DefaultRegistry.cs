using PalladiumDwh.ClientApp.Views;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Core.Services;
using StructureMap;

namespace PalladiumDwh.ClientApp.DependencyResolution {
    public class DefaultRegistry : Registry {

        
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
               scan =>
               {
                   scan.TheCallingAssembly();
                   scan.Assembly("PalladiumDwh.Shared");
                   scan.Assembly("PalladiumDwh.Shared.Data");                  
                   //scan.Assembly("PalladiumDwh.Infrastructure");
                   scan.Assembly("PalladiumDwh.ClientReader.Core");
                   scan.Assembly("PalladiumDwh.ClientReader.Infrastructure");
                   scan.Assembly("PalladiumDwh.ClientUploader.Core");
                   scan.Assembly("PalladiumDwh.ClientUploader.Infrastructure");
                   scan.WithDefaultConventions();
               });

            For<DwapiRemoteContext>().Use<DwapiRemoteContext>()
                .SelectConstructor(() => new DwapiRemoteContext());


            For<IPushProfileService>().Use<PushProfileService>()
                .Ctor<string>("baseUrl").Is(Properties.Settings.Default.dwapiUrl);
        }
        #endregion
    }
}