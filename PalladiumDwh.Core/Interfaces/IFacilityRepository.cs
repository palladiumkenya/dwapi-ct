using System;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        Guid? GetFacilityIdBCode(int code);
    }
}