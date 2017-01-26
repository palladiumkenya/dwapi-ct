﻿using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        void Clear();
        void ClearBy(IList<int> facilityCodes);
        IEnumerable<Facility> Sync(IList<Facility> facilities);      
    }
}