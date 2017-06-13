
using System;
using System.Data;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IEventHistoryRepository: IClientRepository<EventHistory>
    {
        void UpdateStats(EventHistory eventHistory,StatAction action,int count);
        EventHistory GetStats(Guid extractSettingId);
    }
}