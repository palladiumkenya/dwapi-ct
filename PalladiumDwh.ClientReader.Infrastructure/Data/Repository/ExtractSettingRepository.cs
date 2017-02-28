using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ExtractSettingRepository : ClientRepository<ExtractSetting>, IExtractSettingRepository
    {
        public ExtractSettingRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}