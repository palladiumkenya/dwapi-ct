using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        private readonly DwapiCentralContext _context;

        public FacilityRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public Guid? GetFacilityIdByCode(int code)
        {
            string sql = "SELECT Id FROM Facility WHERE Code = @Code;";
            var facility = _context.GetConnection().QueryFirstOrDefault<FacilityId>(sql, new {Code = code});
            return facility?.Id;
        }
        public Guid? SyncNew(Facility facility)
        {
            var facilityId = GetFacilityIdByCode(facility.Code);

            if (facilityId == Guid.Empty || null == facilityId)
            {
                _context.GetConnection().BulkInsert(facility);
                facilityId = facility.Id;
            }
            return facilityId;
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