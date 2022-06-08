using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Data.Repository;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class SmartActionRegisterRepository : GenericRepository<SmartActionRegister>, ISmartActionRegisterRepository
    {

        private readonly DwapiCentralContext _context;

        public SmartActionRegisterRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Clear(int siteCode)
        {
            var connString = _context.GetConnection().ConnectionString;

            var sql = $@"DELETE FROM {nameof(SmartActionRegister)} 
                      WHERE {nameof(ActionRegister.FacilityId)} IN (SELECT ID FROM Facility WHERE Code = @siteCode)";

            using (var connection = new SqlConnection(connString))
            {
                await connection.ExecuteAsync(
                    sql,
                    new { siteCode });
            }

            return true;
        }

        public void CreateAction(List<SmartActionRegister> actionRegisters)
        {
            if (!actionRegisters.Any())
                return;

            var action = actionRegisters.First().Action;
            var area = actionRegisters.First().Area;
            var connString = _context.GetConnection().ConnectionString;

            using (var connection = new SqlConnection(connString))
            {
                // checkExisitng

                var facilityIds = actionRegisters.Select(x => x.FacilityId).ToList();

                var sqlCheck = $@"SELECT DISTINCT {nameof(SmartActionRegister.FacilityId)} FROM {nameof(SmartActionRegister)} 
                      WHERE {nameof(SmartActionRegister.FacilityId)} IN @facilityIds AND
                            {nameof(SmartActionRegister.Action)} = @action AND
                            {nameof(SmartActionRegister.Area)} = @area
                        ";

                var actionPatientIds =
                    connection.Query<Guid>(sqlCheck,
                        new { facilityIds, action, area }).ToList();

                if (actionPatientIds.Any())
                {
                    var newActions = actionRegisters.Where(x => !actionPatientIds.Contains(x.FacilityId)).ToList();

                    if (newActions.Any())
                        connection.BulkInsert(newActions);
                }
                else
                {
                    connection.BulkInsert(actionRegisters);
                }
            }
        }
    }
}