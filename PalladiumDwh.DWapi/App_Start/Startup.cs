using Hangfire;
using Hangfire.SqlServer;
using Hangfire.StructureMap;
using log4net;
using Microsoft.Owin;
using Owin;
using PalladiumDwh.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Z.Dapper.Plus;

[assembly: OwinStartup(typeof(PalladiumDwh.DWapi.Startup))]

namespace PalladiumDwh.DWapi
{
    public class Startup
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Configuration(IAppBuilder app)
        {

            Log.Debug("PalladiumDwh.DWapi starting...");

            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Hangfire.GlobalConfiguration.Configuration
                .UseStructureMapActivator(PalladiumDwh.DWapi.StructuremapMvc.DwapiIContainer)
                .UseBatches()
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("DWAPICentralHangfire", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(10),
                    SlidingInvisibilityTimeout = TimeSpan.FromHours(1),
                    QueuePollInterval = TimeSpan.FromSeconds(40),
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                });


            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            var options = new BackgroundJobServerOptions
            {
                WorkerCount = GetWorkerCount(),
                Queues = new[] { "alpha", "beta", "gamma", "delta", "omega", "default" },
                ShutdownTimeout = TimeSpan.FromMinutes(2),
            };

            app.UseHangfireServer(options);

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
                Log.Error("[Dapper.Plus]", e);
                throw;
            }

            try
            {
                Log.Debug("Upgrading DB..");
                //var service = StructuremapMvc.DwapiIContainer.GetInstance<IAppService>();
                //service.UpgradeDatabase();
                Log.Debug("DB up-to-date");
            }
            catch (Exception e)
            {
                Log.Error("CANNOT UPGRADE DATABASE !", e);
            }


            /*
            _scheduler = StructuremapMvc.DwapiIContainer.GetInstance<IMessengerScheduler>();
            _scheduler.Start();
            */
            Log.Debug("PalladiumDwh.DWapi started!");


        }

        public void WriteDebug(string str)
        {
            Debug.WriteLine(str);
        }

        public int GetWorkerCount()
        {
            return Properties.Settings.Default.WorkerCount > 0
                ? Properties.Settings.Default.WorkerCount
                : Environment.ProcessorCount * 5;
        }
    }
}
