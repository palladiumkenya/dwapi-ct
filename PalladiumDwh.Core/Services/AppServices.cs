using System.Reflection;
using log4net;
using PalladiumDwh.Core.Interfaces;

namespace PalladiumDwh.Core.Services
{
    public class AppService: IAppService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IAppRepository _appRepository;

        public AppService(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public void UpgradeDatabase()
        {
          _appRepository.RunMigrations();
        }
    }
}