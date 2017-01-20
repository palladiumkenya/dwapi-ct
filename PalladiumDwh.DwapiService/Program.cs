using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.DWapiService.DependencyResolution;
using PalladiumDWh.DwapiService;
using StructureMap;

namespace PalladiumDwh.DWapiService
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

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ExtractService(), 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
