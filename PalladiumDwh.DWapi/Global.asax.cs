using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using PalladiumDwh.Core.Interfaces;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace PalladiumDwh.DWapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IMessengerScheduler _scheduler;

        protected void Application_Start()
        {
            Log.Debug("PalladiumDwh.DWapi starting...");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _scheduler = StructuremapMvc.DwapiIContainer.GetInstance<IMessengerScheduler>();
            _scheduler.Start();
            Log.Debug("PalladiumDwh.DWapi started!");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _scheduler.Shutdown();
        }

    }
}
