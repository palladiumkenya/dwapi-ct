using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using CsvHelper;
using Dapper;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
    public abstract class LoadExtractCsvCommand<T> : ILoadExtractCommand<T> where T : TempExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal readonly IDbConnection CleintConnection;
        internal readonly string CommandText;
        internal readonly int BatchSize;
        protected LoadExtractCsvCommand(IDbConnection clientConnection,string commandText, int batchSize = 100)
        {
            CleintConnection = clientConnection;
            CommandText = commandText;
            BatchSize = batchSize;
        }

        public virtual void Execute()
        {
                    
                    using (TextReader txtReader =File.OpenText(CommandText))
                    {

                        var reader = new CsvReader(txtReader);
                        if (null != reader)
                        {
                            var extracts = new List<T>();
                            int count = 0;
                            var extract = (T) Activator.CreateInstance(typeof(T));
                            string action = extract.GetAddAction();

                            while (reader.Read())
                            {

                                count++;
                                extract = reader.GetRecord<T>();
                                
                                if (!extract.HasError)
                                {
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

                            }

                            if (extracts.Count > 0)
                            {
                                
                                if (CleintConnection.State != ConnectionState.Open)
                                {
                                    CleintConnection.Open();
                                }
                                var tx = CleintConnection.BeginTransaction();
                                CleintConnection.Execute(action, extracts, tx);
                                tx.Commit();
                                
                            }
                        }
                    }

                

            
        }

        private void LoadData(List<T> extracts)
        {


        }
    }
}
