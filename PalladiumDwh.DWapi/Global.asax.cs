using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using log4net;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace PalladiumDwh.DWapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private BackgroundJobServer _server;
        protected void Application_Start()
        {
            Log.Debug("PalladiumDwh.DWapi starting...");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _server = new BackgroundJobServer();
            Log.Debug("PalladiumDwh.DWapi started!");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _server.Dispose();
        }

    }
}
