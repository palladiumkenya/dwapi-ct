using System.Reflection;
using System.ServiceProcess;
using log4net;
using PalladiumDWh.DwapiService.DependencyResolution;
using StructureMap;

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
            Log.Debug("Loading DWapiService...");
            IOC = IoC.Initialize();

            var servicesToRun = new ServiceBase[]
            {
                new ExtractService()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
