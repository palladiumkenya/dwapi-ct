using PalladiumDwh.Core.Interfaces;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class AppRepository : IAppRepository
    {
        private readonly DwapiCentralContext context;

        public AppRepository(DwapiCentralContext context)
        {
            this.context = context;
        }

        public void RunMigrations()
        {
            context.UpgradeDb();
        }
    }
}