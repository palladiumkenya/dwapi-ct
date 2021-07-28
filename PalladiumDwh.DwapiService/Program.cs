using System;
using System.Reflection;
using System.ServiceProcess;
using log4net;
using PalladiumDWh.DwapiService.DependencyResolution;
using StructureMap;
using Z.BulkOperations;
using Z.Dapper.Plus;

namespace PalladiumDWh.DwapiService
{
    static class Program
    {
        public static IContainer IOC;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Log.Debug(new string('*',40));
            Log.Debug($"{Assembly.GetExecutingAssembly().GetName().Name}");
            Log.Debug($"{Assembly.GetExecutingAssembly().GetName().Version}");
            Log.Debug($"Rel: 16MAR21 1451");
            Log.Debug(new string('*', 40));
            Log.Debug("Loading DWapiService...");

            // CHECK if the license if valid for the default provider (SQL Server)
            try
            {
                DapperPlusManager.AddLicense(Properties.Settings.Default.Z_Dapper_Plus_LicenseName,Properties.Settings.Default.Z_Dapper_Plus_LicenseKey);
                if (!Z.Dapper.Plus.DapperPlusManager.ValidateLicense(out var licenseErrorMessage))
                {
                    throw new Exception(licenseErrorMessage);
                }
                Log.Debug("Loading DWapiService [Dapper.Plus]...OK");

            }

            catch (Exception e)
            {
                Log.Debug(e);
                throw;
            }




            IOC = IoC.Initialize();
#if(!DEBUG)
            var servicesToRun = new ServiceBase[]
            {
                new ExtractService()
            };

            ServiceBase.Run(servicesToRun);
#else
            ExtractService myServ = new ExtractService();
            myServ.RunSvc();
#endif
        }
    }
}
