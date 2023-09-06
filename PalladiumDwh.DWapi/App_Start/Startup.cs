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
using System.Linq;
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
                .UseBatches(TimeSpan.FromDays(Properties.Settings.Default.WorkerBatchRetention))
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("DWAPICentralHangfire", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(Properties.Settings.Default.WorkerComandTimeout),
                    SlidingInvisibilityTimeout =
                        TimeSpan.FromMinutes(Properties.Settings.Default.WorkerInvisibilityTimeout),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                });


            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            var queues = new List<string>
            {
                "manifest", "patient", "patientart", "patientpharmacy", "patientvisits", "patientstatus", 
                "covid","defaultertracing", "patientlabs", "patientbaselines", "patientadverseevents", "otz", "ovc",
                "depressionscreening", "drugalcoholscreening", "enhancedadherencecounselling", "gbvscreening", "ipt",
                "allergieschronicillness", "contactlisting", "default", "cervicalcancerscreening", "iitriskscores"
            };
            queues.ForEach(queue => ConfigureWorkers(app, new[] { queue.ToLower() }));

            // CHECK if the license if valid for the default provider (SQL Server)
            try
            {
                DapperPlusManager.AddLicense(Properties.Settings.Default.Z_Dapper_Plus_LicenseName,
                    Properties.Settings.Default.Z_Dapper_Plus_LicenseKey);
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

        private void ConfigureWorkers(IAppBuilder app, string[] queues)
        {

            var hangfireQueueOptions = new BackgroundJobServerOptions
            {
                ServerName = $"{Environment.MachineName}:{queues[0].ToUpper()}",
                WorkerCount = GetWorkerCount(queues[0]),
                Queues = queues,
                ShutdownTimeout = TimeSpan.FromMinutes(2),
            };

            app.UseHangfireServer(hangfireQueueOptions);
        }

        private int GetWorkerCount(string queue)
        {
            int count = 5;

            try
            {
                var workerCount = Properties.Settings.Default.WorkerCount;
                var workers = workerCount.Split(',').ToList();
                var worker = workers.FirstOrDefault(x => x.Contains(queue));
                if (null != worker)
                    int.TryParse(worker.Split('-')[1], out count);
            }
            catch (Exception e)
            {
                Log.Error("Error reading worker count", e);
            }

            return count;
        }
    }
}
