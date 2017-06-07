using System.Reflection;
using System.Threading.Tasks;
using DbUp;
using PalladiumDwh.ClientApp.Views;
using System.Configuration;

namespace PalladiumDwh.ClientApp.Presenters
{
    public class StartupPresenter:IStartupPresenter
    {
        public IStartupView View { get; }

        public StartupPresenter(IStartupView view)
        {
            View = view;
            View.Presenter = this;
        }

        public void Initialize()
        {
            //throw new System.NotImplementedException();
        }

        public Task<bool> UpdateDatabase()
        {
            View.Status = "Checking database...";

           return Task.Run(() =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DWAPIRemote"].ToString();

                EnsureDatabase.For.SqlDatabase(connectionString);

                var upgrader =
                    DeployChanges.To
                        .SqlDatabase(connectionString)
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                        .Build();

                var result = upgrader.PerformUpgrade();

                return result.Successful;
            });

        }

        public void LoadDashboard()
        {
            View.ShowDash();
        }
    }

   

}