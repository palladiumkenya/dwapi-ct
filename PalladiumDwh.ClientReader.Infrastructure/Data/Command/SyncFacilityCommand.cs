using System.Data;
using System.Data.SqlClient;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncFacilityCommand :  ISyncFacilityCommand
  {
      private readonly string  _connectionString;

      public SyncFacilityCommand(string connectionString)
      {
            _connectionString = connectionString;
        }

      public void Execute()
      {
            var extract = new ClientFacility();
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = extract.GetAddAction(nameof(ClientPatientExtract));
                   command.ExecuteNonQuery();
                }

            }
        }
  }
}
