using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;
using Dapper;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public abstract class   LoadExtractDbCommand<T> : ILoadExtractCommand<T> where T : TempExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal readonly IDbConnection SourceConnection;
        internal readonly IDbConnection CleintConnection;
        internal readonly string CommandText;
        internal readonly int BatchSize;

        protected LoadExtractDbCommand(IDbConnection sourceConnection, IDbConnection clientConnection,string commandText,int batchSize=100)
        {
            SourceConnection = sourceConnection;
            CleintConnection = clientConnection;
            CommandText = commandText;
            BatchSize = batchSize;
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
                            var extracts = new List<T>();
                            int count = 0;
                            var extract = (T)Activator.CreateInstance(typeof(T));
                            string action = extract.GetAddAction();

                            while (reader.Read())
                            {
                                count++;
                                extract = (T)Activator.CreateInstance(typeof(T));
                                extract.Load(reader);
                                if (BatchSize == 0)
                                {
                                    if (CleintConnection.State != ConnectionState.Open)
                                    {
                                        CleintConnection.Open();
                                    }
                                    CleintConnection.Execute(action, extract);
                                }
                                else
                                {
                                    extracts.Add(extract);

                                    if (count == BatchSize && BatchSize > 0)
                                    {
                                        if (CleintConnection.State != ConnectionState.Open)
                                        {
                                            CleintConnection.Open();
                                        }

                                        var tx = CleintConnection.BeginTransaction();
                                        CleintConnection.Execute(action, extracts, tx);
                                        tx.Commit();
                                        extracts = new List<T>();
                                        count = 0;
                                    }
                                }

                            }

                            if (extracts.Count > 0)
                            {
                                if (CleintConnection.State != ConnectionState.Open)
                                {
                                    CleintConnection.Open();
                                }
                                var tx = CleintConnection.BeginTransaction();
                                CleintConnection.Execute(action, extracts,tx);
                                tx.Commit();
                            }
                        }
                    }
                    
                }

            }
        }

        private void LoadData(List<T> extracts)
        {


        }
    }
}
