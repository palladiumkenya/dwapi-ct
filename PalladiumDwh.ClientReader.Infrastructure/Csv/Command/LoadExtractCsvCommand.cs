using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using log4net;
using PalladiumDwh.ClientReader.Core.Enums;
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
        internal readonly SqlConnection CleintConnection;
        internal string CommandText;
        internal readonly int BatchSize;
        private  LoadSummary _summary;
        private ExtractSetting _extractSetting;

        protected LoadExtractCsvCommand(IEMRRepository emrRepository, int batchSize = 100)
        {
            _emrRepository = emrRepository;
            CleintConnection = _emrRepository.GetConnection() as SqlConnection;
            BatchSize = batchSize;
        }

        public LoadSummary Summary => _summary;

        public async Task<LoadSummary> ExecuteAsync(string csvExtract, IProgress<DProgress> progress = null)
        {
            _summary = new LoadSummary();
            var extractName = typeof(T).Name;

            CommandText = csvExtract;
            if (string.IsNullOrWhiteSpace(CommandText)) throw new Exception($"missing csv file found!");

            var emr = _emrRepository.GetDefault();
            if (null == emr) throw new Exception($"No Default EMR Setup !");

            _extractSetting = emr.GetActiveExtractSetting($"{extractName}");
            if (null == _extractSetting) throw new Exception($"No Extract Setting found for {emr}");

            int totalRecords = 0;
            progress?.ReportStatus($"Reading Csv...",null,null,_extractSetting);

            EventHistory currentHistory = _emrRepository.GetStats(_extractSetting.Id);

            totalRecords = currentHistory.Found ?? await GetTotal(CommandText);
            totalRecords += 1;

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
                    int totalcount = 0;
                    while (reader.Read())
                    {
                        totalcount++;
                        count++;
                        progress?.ReportStatus($"Reading Csv [{Path.GetFileName(CommandText)}]...", totalcount, totalRecords, _extractSetting);

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

                        if (BatchSize == 0)
                        {
                            try
                            {
                                if (CleintConnection.State != ConnectionState.Open)
                                {
                                    await CleintConnection.OpenAsync();
                                }
                                var tx = CleintConnection.BeginTransaction(IsolationLevel.RepeatableRead);
                                loaded += await CleintConnection.ExecuteAsync(action, extract, tx, 0);
                                tx.Commit();

                                //update stats
                                _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Loaded, loaded);

                            }
                            catch (Exception e)
                            {
                                Log.Debug(e);
                                throw;
                            }
                           
                        }
                        else
                        {
                            extracts.Add(extract);

                            if (count == BatchSize && BatchSize > 0)
                            {
                                try
                                {
                                    if (CleintConnection.State != ConnectionState.Open)
                                    {
                                        await CleintConnection.OpenAsync();
                                    }
                                    var tx = CleintConnection.BeginTransaction(IsolationLevel.RepeatableRead);
                                    loaded += await CleintConnection.ExecuteAsync(action, extract, tx, 0);
                                    tx.Commit();

                                    //update stats
                                    _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Loaded, loaded);
                                }
                                catch (Exception e)
                                {
                                    Log.Debug(e);
                                    throw;
                                }
                                
                                extracts = new List<T>();
                                count = 0;
                            }
                        }

                        
                    }

                    if (extracts.Count > 0)
                    {
                        try
                        {
                            if (CleintConnection.State != ConnectionState.Open)
                            {
                                await CleintConnection.OpenAsync();
                            }
                            var tx = CleintConnection.BeginTransaction(IsolationLevel.RepeatableRead);
                            loaded += await CleintConnection.ExecuteAsync(action, extract, tx, 0);
                            tx.Commit();

                            //update stats
                            _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Loaded, loaded);
                        }
                        catch (Exception e)
                        {
                            Log.Debug(e);
                            throw;
                        }
                        
                    }
                    progress?.ReportStatus($"Reading Csv [{Path.GetFileName(CommandText)}]...", 1, 1);
                    currentHistory = _emrRepository.GetStats(_extractSetting.Id);

                    _summary.Loaded = currentHistory.Loaded ?? loaded;
                    _summary.Total = currentHistory.Found ?? totalcount;

                    progress?.ReportStatus($"Reading Csv [{Path.GetFileName(CommandText)}] Finished", null, null, _extractSetting);
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

        private async Task<int> GetTotal(string csv)
        {
            var totalRecords = 0;

            try
            {
                await Task.Run(() =>
                {
                    totalRecords = File.ReadAllLines(csv).Length;
                });
            }
            catch (Exception e)
            {
                Log.Debug(e);
            }

            return totalRecords;

        }
    }
}
