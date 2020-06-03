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
    public class ActionRegisterRepository : GenericRepository<ActionRegister>, IActionRegisterRepository
    {

        private readonly DwapiCentralContext _context;

        public ActionRegisterRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Clear(int siteCode)
        {
            var connString = _context.GetConnection().ConnectionString;

            var sql = $@"DELETE FROM {nameof(ActionRegister)} 
                      WHERE {nameof(ActionRegister.FacilityId)} IN (SELECT ID FROM Facility WHERE Code = @siteCode)";

            using (var connection = new SqlConnection(connString))
            {
               await  connection.ExecuteAsync(
                    sql,
                    new { siteCode });
            }

            return true;
        }

        public void CreateAction(List<ActionRegister> actionRegisters)
        {
            var connString = _context.GetConnection().ConnectionString;

            using (var connection = new SqlConnection(connString))
            {
                List<ActionRegister> actions=new List<ActionRegister>();

                foreach (var actionRegister in actionRegisters)
                {

                    // check if exists

                    var sql = $@"SELECT * FROM {nameof(ActionRegister)} 
                      WHERE {nameof(ActionRegister.PatientId)} = @patientId AND
                            {nameof(ActionRegister.Action)} = @action AND
                            {nameof(ActionRegister.Area)} = @area
                        ";

                    var action =
                        connection.QueryFirstOrDefault<ActionRegister>(sql,
                            new
                            {
                                patientId = actionRegister.PatientId, action = actionRegister.Action,
                                area = actionRegister.Area
                            });

                    if (null == action)
                    {
                        actions.Add(actionRegister);
                    }
                }

                connection.BulkInsert(actions);
            }
        }
    }
}