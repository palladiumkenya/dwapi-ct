using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using Dapper;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class SyncRepository : ISyncRepository
    {
        private readonly string _connectionString;

        public SyncRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SyncPatientData<TSrc, TDest>() where TSrc : TempPatientExtract where TDest : ClientPatientExtract
        {
            var extract = (TDest) Activator.CreateInstance(typeof(TDest));
            using (var SourceConnection = new SqlConnection(_connectionString))
            {
                if (SourceConnection.State != ConnectionState.Open)
                {
                    SourceConnection.Open();
                }

                using (var command = SourceConnection.CreateCommand())
                {
                    command.CommandText = extract.GetAddAction(typeof(TSrc).FullName);
                    command.ExecuteNonQuery();
                }

            }
        }
    }
}