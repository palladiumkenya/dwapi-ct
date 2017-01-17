using System;
using System.Linq.Expressions;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        private readonly DwapiCentralContext _context;

        public FacilityRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public Guid? GetFacilityIdBCode(int code)
        {
            return Find(x => x.Code == code)?.Id;
        }

        public Guid? Sync(Facility facility)
        {
            var facilityId = GetFacilityIdBCode(facility.Code);

            if (facilityId == Guid.Empty || null == facilityId)
            {
                Insert(facility);
                CommitChanges();

            }
            return facilityId;
        }
    }
}