using System;
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
            else
            {
                Update(facility);
            }
            return facilityId;
        }
    }
}