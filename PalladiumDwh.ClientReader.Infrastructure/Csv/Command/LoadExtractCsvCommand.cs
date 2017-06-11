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
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Model.Source.Map;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
    public abstract class LoadExtractCsvCommand<T> : ILoadCsvExtractCommand<T> where T : TempExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IEMRRepository _emrRepository;
        internal readonly IDbConnection CleintConnection;
        internal string CommandText;
        internal readonly int BatchSize;
        private  LoadSummary _summary;

        protected LoadExtractCsvCommand(IEMRRepository emrRepository, int batchSize = 100)
        {
            _emrRepository = emrRepository;
            CleintConnection = _emrRepository.GetConnection();
            BatchSize = batchSize;
        }

        public LoadSummary Summary => _summary;

        public async Task<LoadSummary> ExecuteAsync(string csvExtract, Progress<DProgress> progress = null)
        {
            CommandText = csvExtract;
            _summary = new LoadSummary();
            int totalRecords = 0;

            progress?.ReportStatus($"Reading Csv...");

            try
            {
                await Task.Run(() =>
                {
                    totalRecords = File.ReadAllLines(CommandText).Length;
                });
            }
            catch (Exception e)
            {
                Log.Debug(e);
            }


            using (TextReader txtReader = File.OpenText(CommandText))
            {

                var reader = new CsvReader(txtReader, GetConfig());
                reader.Configuration.RegisterClassMap(TempExtractMap.GetMap<T>());

                if (null != reader)
                {
                    var extracts = new List<T>();
                    int count = 0;
                    int loaded = 0;
                    var extract = (T)Activator.CreateInstance(typeof(T));
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

                        progress?.ReportStatus($"Reading Csv [{Path.GetFileName(CommandText)}]...", count, totalRecords);
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

            return _summary;
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
