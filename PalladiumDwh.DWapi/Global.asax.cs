using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using Hangfire.SqlServer;
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

            JobStorage.Current = new SqlServerStorage("DWAPICentral", new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            });
            _server = new BackgroundJobServer();
            Log.Debug("PalladiumDwh.DWapi started!");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _server.Dispose();
        }

    }
}
