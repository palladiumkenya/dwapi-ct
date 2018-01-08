using System;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        Guid? GetFacilityIdBCode(int code);
        Guid? GetFacilityIdByCode(int code);
        Guid? Sync(Facility facility);
        Guid? SyncNew(Facility facility);
  }
}