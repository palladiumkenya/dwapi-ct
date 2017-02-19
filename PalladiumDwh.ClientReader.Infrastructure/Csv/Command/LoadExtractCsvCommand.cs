using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Model.Source.Map;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
    public abstract class LoadExtractCsvCommand<T> : ILoadExtractCommand<T> where T : TempExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal readonly IDbConnection CleintConnection;
        internal readonly string CommandText;
        internal readonly int BatchSize;
        private  LoadSummary _summary;

        protected LoadExtractCsvCommand(IDbConnection clientConnection, string commandText, int batchSize = 100)
        {
            CleintConnection = clientConnection;
            CommandText = commandText;
            BatchSize = batchSize;
        }

        public LoadSummary Summary => _summary;

        public virtual void Execute()
        {
            _summary = new LoadSummary();

            using (TextReader txtReader = File.OpenText(CommandText))
            {

                var reader = new CsvReader(txtReader, GetConfig());
                reader.Configuration.RegisterClassMap(TempExtractMap.GetMap<T>());

                if (null != reader)
                {
                    var extracts = new List<T>();
                    int count = 0;
                    int loaded = 0;
                    var extract = (T) Activator.CreateInstance(typeof(T));
                    string action = extract.GetAddAction();

                    while (reader.Read())
                    {

                        count++;
                        _summary.Total = count;
                        try
                        {
                            extract = reader.GetRecord<T>();
                            //extract.Load(reader);

                        }
                        catch (Exception ex)
                        {
                            Log.Debug(string.Join("^", reader.CurrentRecord));
                            Log.Debug(ex);
                            try
                            {
                                Log.Debug(ex.Data["CsvHelper"]);
                            }
                            catch
                            {
                            }
                            //extract.HasError = true;
                        }


                        if (!extract.HasError)
                        {
                            loaded++;
                            _summary.Loaded = loaded;
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

        public Task<LoadSummary> ExecuteAsync(Progress<ProcessStatus> progressPercent = null)
        {
            throw new NotImplementedException();
        }

        private CsvConfiguration GetConfig()
        {
            return new CsvConfiguration()
            {
                IsHeaderCaseSensitive = false,
                WillThrowOnMissingField = false,
                SkipEmptyRecords = true,
                IgnoreHeaderWhiteSpace = true,
                TrimFields = true,
                TrimHeaders = true
            };

        }
    }
}
