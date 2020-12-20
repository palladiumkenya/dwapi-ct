using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using PalladiumDwh.Core.Interfaces;
using Z.Dapper.Plus;
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

            // CHECK if the license if valid for the default provider (SQL Server)
            try
            {
                DapperPlusManager.AddLicense(Properties.Settings.Default.Z_Dapper_Plus_LicenseName, Properties.Settings.Default.Z_Dapper_Plus_LicenseKey);
                if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
                {
                    throw new Exception(licenseErrorMessage);
                }
                Log.Debug("Loading DWapiService [Dapper.Plus]...OK");
            }

            catch (Exception e)
            {
                Log.Error("[Dapper.Plus]",e);
                throw;
            }

            /*
            _scheduler = StructuremapMvc.DwapiIContainer.GetInstance<IMessengerScheduler>();
            _scheduler.Start();
            */
            Log.Debug("PalladiumDwh.DWapi started!");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _scheduler?.Shutdown();
        }

    }
}
