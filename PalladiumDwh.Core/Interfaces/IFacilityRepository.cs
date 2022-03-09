using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        Guid? GetFacilityIdByCode(int code);
        Guid? SyncNew(Facility facility);

        Guid? GetFacilityIdBCode(int code);
        MasterFacility GetFacilityByCode(int code);
        Guid? Sync(Facility facility);

        IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds);
        StatsDto GetFacStats(Guid facilityId);
        void Enroll(MasterFacility masterFacility,string emr,bool allowSnapshot);
        void EndSession(Guid session);
        IEnumerable<HandshakeDto> GetSessionHandshakes(Guid session);
        List<FacilityCacheDto> ReadFacilityCache();
    }
}
