using System;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

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
                facilityId = facility.Id;
            }
            return facilityId;
        }
    }
}