using System;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class EventHistoryRepository: ClientRepository<EventHistory>,IEventHistoryRepository
    {
        public EventHistoryRepository(DwapiRemoteContext context) : base(context)
        {
        }

        public void UpdateStats(EventHistory eventHistory, StatAction action, int count)
        {
            if (action == StatAction.Found)
            {
                eventHistory.Found = count;
                Insert(eventHistory);
            }
            if (action == StatAction.Loaded)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.Loaded = count;
                    eventHistoryToUpdate.LoadDate=DateTime.Now;
                    Update(eventHistoryToUpdate);
                }
            }
            if (action == StatAction.Rejected)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.Rejected = count;
                    Update(eventHistoryToUpdate);
                }
            }

            if (action == StatAction.Sent)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.Sent = count;
                    eventHistoryToUpdate.SendDate = DateTime.Now;
                    Update(eventHistoryToUpdate);
                }
            }

            if (action == StatAction.NotSent)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.NotSent = count;
                    Update(eventHistoryToUpdate);
                }
            }

        }

        public EventHistory GetStats(Guid extractSettingId)
        {
            return DbSet.FirstOrDefault(x => x.ExtractSettingId == extractSettingId);
        }
    }
}