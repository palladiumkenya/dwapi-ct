
using System;
using System.Data;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IEMRRepository: IClientRepository<EMR>
    {
        EMR GetDefault();
        void SetEmrAsDefault(Guid id);
        IDbConnection GetConnection();
        IDbConnection GetEmrConnection();
        int CreateStats(EventHistory eventHistory, StatAction action);
        int UpdateStats(Guid extractSettingId, StatAction action, int count);
        EventHistory GetStats(Guid extractSettingId);
        Task<EventHistory> GetStatsAsync(Guid extractSettingId);

    }
}