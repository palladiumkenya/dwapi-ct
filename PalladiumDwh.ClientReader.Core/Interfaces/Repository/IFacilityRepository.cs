using System;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        Guid? GetFacilityIdBCode(int code);
        Guid? Sync(Facility facility);      
    }
}