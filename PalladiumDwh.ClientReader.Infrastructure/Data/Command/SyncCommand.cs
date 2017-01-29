using System;
using System.Data;
using System.Data.SqlClient;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class SyncCommand<TS,TD> : ISyncCommand<TS, TD> where TS:TempExtract where TD:ClientExtract
    {
        private readonly string _connectionString;

        public SyncCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Execute()
        {
            var extract = (TD)Activator.CreateInstance(typeof(TD));
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = extract.GetAddAction(typeof(TS).Name);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}