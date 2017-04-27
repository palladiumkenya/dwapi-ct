using System;
using System.Reflection;
using System.Windows.Forms;
using log4net;
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
            Log.Debug("Loading DWapi...");
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new Dashboard());
        }
    }
}
