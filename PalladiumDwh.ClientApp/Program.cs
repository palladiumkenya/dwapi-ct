using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using PalladiumDwh.ClientApp.DependencyResolution;
using PalladiumDwh.ClientApp.Views;
using StructureMap;

namespace PalladiumDwh.ClientApp
{
    static class Program
    {
        public static IContainer IOC;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Debug("Loading DWapiService...");
            IOC = IoC.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Dashboard());
        }
    }
}
