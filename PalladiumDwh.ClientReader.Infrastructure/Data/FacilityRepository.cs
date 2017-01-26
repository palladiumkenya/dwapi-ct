using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        private readonly DwapiRemoteContext _context;

        public FacilityRepository(DwapiRemoteContext context) : base(context)
        {
            _context = context;
        }

        public void Clear()
        {
            var facilitys = GetAll();
            _context.Facilities.RemoveRange(facilitys);
            _context.SaveChanges();
        }

        public void ClearBy(IList<int> facilityCodes)
        {
            var deleteList = GetAll()
                .Where(x => facilityCodes.Contains(x.Code));

            _context.Facilities.RemoveRange(deleteList);
            _context.SaveChanges();
        }

        public IEnumerable<Facility> Sync(IList<Facility> facilities)
        {
            Insert(facilities);
            CommitChanges();
            return facilities;
        }
    }
}