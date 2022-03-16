using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.StructureMap;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.DWapi.Helpers;
using PalladiumDwh.Infrastructure.Data;
using Z.Dapper.Plus;

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
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HangfireAspNet.Use(GetHangfireServers);

            BatchJob.StartNew(x =>
            {
                x.Enqueue(() => WriteDebug("Dwapi v3.0.0.0 Background Jobs Started!"));
            });


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

            try
            {
                Log.Debug("Upgrading DB..");
                //var service = StructuremapMvc.DwapiIContainer.GetInstance<IAppService>();
                //service.UpgradeDatabase();
                Log.Debug("DB up-to-date");
            }
            catch (Exception e)
            {
                Log.Error("CANNOT UPGRADE DATABASE !",e);
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


        private IEnumerable<IDisposable> GetHangfireServers()
        {
            Hangfire.GlobalConfiguration.Configuration
                .UseStructureMapActivator(PalladiumDwh.DWapi.StructuremapMvc.DwapiIContainer)
                .UseBatches()
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("DWAPICentralHangfire", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            var options = new BackgroundJobServerOptions
            { 
                WorkerCount = Environment.ProcessorCount * 5,
                Queues = new[] { "alpha", "beta", "default" }
            };
            yield return new BackgroundJobServer(options);
        }

        public  void WriteDebug(string str)
        {
            Debug.WriteLine(str);
        }
    }
}
