﻿using System;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        Guid? GetFacilityIdBCode(int code);
        Guid? Sync(Facility facility);
    }
}