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
    public abstract class LoadExtractDbCommand<T> : ILoadExtractCommand<T> where T : TempExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal readonly IDbConnection SourceConnection;
        internal readonly IDbConnection CleintConnection;
        internal readonly string CommandText;


        protected LoadExtractDbCommand(IDbConnection sourceConnection, IDbConnection clientConnection,
            string commandText)
        {
            SourceConnection = sourceConnection;
            CleintConnection = clientConnection;
            CommandText = commandText;
        }

        public virtual void Execute()
        {
            using (SourceConnection)
            {
                if (SourceConnection.State != ConnectionState.Open)
                {
                    SourceConnection.Open();
                }

                using (var command = SourceConnection.CreateCommand())
                {
                    command.CommandText = CommandText;
                    using (var reader = command.ExecuteReader())
                    {

                        if (null != reader)
                        {
                            int batch = 0;
                            var extractList = new List<T>();
                            while (reader.Read())
                            {
                                batch++;
                                var extract = (T) Activator.CreateInstance(typeof(T));
                                extract.Load(reader);
                                extractList.Add(extract);
                                if (batch == 100)
                                {
                                    //Insert
                                    LoadData(extractList);
                                    extractList = new List<T>();
                                    batch = 0;
                                }
                            }
                        }
                    }
                    ;
                }

            }
        }

        private void LoadData(List<T> extracts)
        {


        }
    }
}
