using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public abstract class LoadExtractDbCommand<T> : ILoadExtractCommand<T> where T:TempExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal readonly IDbConnection SourceConnection;
        internal readonly IDbConnection CleintConnection;
        internal readonly string CommandText;

        protected LoadExtractDbCommand(IDbConnection sourceConnection,  string commandText)
        {
            SourceConnection = sourceConnection;
            //CleintConnection = clientConnection;
            CommandText = commandText;
        }
        protected LoadExtractDbCommand(IDbConnection sourceConnection,IDbConnection clientConnection, string commandText)
        {
            SourceConnection = sourceConnection;
            CleintConnection = clientConnection;
            CommandText = commandText;
        }

        public virtual IEnumerable<T> Execute()
        {
            var extractList = new List<T>();
            using (SourceConnection)
            {
                if (SourceConnection.State != ConnectionState.Open)
                {
                    SourceConnection.Open();
                }
                using (var command = SourceConnection.CreateCommand())
                {
                    command.CommandText = CommandText;
                  
                    var reader = command.ExecuteReader();
                    LoadData<T>(reader);
                    /*
                    while (reader.Read())
                    {
                        var extract =(T) Activator.CreateInstance(typeof(T));
                        extract.Load(reader);
                        extractList.Add(extract);
                    }
                    */
                }
            }
            return extractList;
        }

        private void LoadData<T>(IDataReader reader)
        {
            using (SqlBulkCopy bulkCopy =new SqlBulkCopy(CleintConnection.ConnectionString))
            {
                bulkCopy.DestinationTableName = $"dbo.{nameof(T)}";

                Log.Debug($"loading {bulkCopy.DestinationTableName}...");
                try
                {
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(reader);
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Close the SqlDataReader. The SqlBulkCopy
                    // object is automatically closed at the end
                    // of the using block.
                    reader.Close();
                }
            }
        }

      
    }
}

/*
 IDbDataParameter p = command.CreateParameter();
 p.ParameterName = "@patientpk";
 p.DbType = DbType.Int32;
 p.Value = patientPk;
 command.Parameters.Add(p);
  */
