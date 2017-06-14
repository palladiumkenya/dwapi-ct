using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;
using Dapper;
using log4net;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class LoadExtractCommand<T> : ILoadExtractCommand<T> where T : TempExtract, new()
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IEMRRepository _emrRepository;
        private readonly SqlConnection _connection;
        private readonly IDbConnection _emrConnection;
        private readonly int _batchSize;
        private ExtractSetting _extractSetting;
        private LoadSummary _summary;
        private IProgress<ProcessStatus> _progress;
        private int recordCount = 0;
        ProcessStatus _processStatus=new ProcessStatus();

        public LoadExtractCommand(IEMRRepository emrRepository, int batchSize = 2000)
        {
            _emrRepository = emrRepository;
            _batchSize = batchSize;
            _connection = _emrRepository.GetConnection() as SqlConnection; 
            _emrConnection = _emrRepository.GetEmrConnection();
        }

        public LoadSummary Summary => _summary;

        public virtual void Execute()
        {
            _summary = new LoadSummary();

            string extractName = typeof(T).Name;
            string commandText = string.Empty;

            var emr = _emrRepository.GetDefault();

            if (null == emr) throw new Exception($"No Default EMR Setup !");

            _extractSetting = emr.GetActiveExtractSetting($"{extractName}");

            if (null == _extractSetting) throw new Exception($"No Extract Setting found for {emr}");

            commandText = _extractSetting.ExtractSql;

            if (string.IsNullOrWhiteSpace(commandText)) throw new Exception($"No sql command found for {extractName}");

            using (_emrConnection)
            {
                if (_emrConnection.State != ConnectionState.Open)
                {
                    _emrConnection.Open();
                }

                using (var command = _emrConnection.CreateCommand())
                {
                    command.CommandText = commandText;
                    using (var reader = command.ExecuteReader())
                    {
                        if (null != reader)
                        {
                            var extracts = new List<T>();
                            int totalcount = 0;
                            int count = 0;
                            int loaded = 0;
                            var extract = new T();
                            string action = extract.GetAddAction();

                            while (reader.Read())
                            {
                                totalcount++;
                                count++;

                                extract = new T();
                                extract.Load(reader);

                                loaded++;

                                if (_batchSize == 0)
                                {

                                    if (_connection.State != ConnectionState.Open)
                                    {
                                        _connection.Open();
                                    }
                                    _connection.Execute(action, extract);
                                }
                                else
                                {
                                    extracts.Add(extract);

                                    if (count == _batchSize && _batchSize > 0)
                                    {

                                        if (_connection.State != ConnectionState.Open)
                                        {
                                            _connection.Open();
                                        }

                                        var tx = _connection.BeginTransaction();
                                        _connection.Execute(action, extracts, tx);
                                        tx.Commit();

                                        extracts = new List<T>();
                                        count = 0;
                                    }
                                }


                            }

                            if (extracts.Count > 0)
                            {

                                if (_connection.State != ConnectionState.Open)
                                {
                                    _connection.Open();
                                }
                                var tx = _connection.BeginTransaction();
                                _connection.Execute(action, extracts, tx);
                                tx.Commit();

                            }

                            _summary.Loaded = loaded;
                            _summary.Total = totalcount;
                        }
                    }

                }

            }
        }

        public virtual async Task<LoadSummary> ExecuteAsync(Progress<ProcessStatus> progressPercent = null, IProgress<DProgress> progress = null)
        {
            //progress?.ReportStatus("");

            _progress = progressPercent;
            _summary = new LoadSummary();

            var extractName = typeof(T).Name;
            var commandText = string.Empty;

            Log.Debug($"Executing load {extractName} command...");

            var emr = _emrRepository.GetDefault();
            if (null == emr) throw new Exception($"No Default EMR Setup !");

            _extractSetting = emr.GetActiveExtractSetting($"{extractName}");
            if (null == _extractSetting) throw new Exception($"No Extract Setting found for {emr}");

            commandText = _extractSetting.ExtractSql;
            if (string.IsNullOrWhiteSpace(commandText)) throw new Exception($"No sql command found for {extractName}");

            EventHistory currentHistory = _emrRepository.GetStats(_extractSetting.Id);

            ShowPercentage(1);

            using (_emrConnection)
            {
                if (_emrConnection.State != ConnectionState.Open)
                {
                    _emrConnection.Open();
                }

                using (var command = _emrConnection.CreateCommand())
                {
                    command.CommandText = commandText;

                    IDataReader reader;

                    try
                    {
                        reader = await GetTask(command);
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e);
                        throw;
                    }


                    using (reader)
                    {
                        if (null != reader)
                        {
                            var extracts = new List<T>();
                            int totalcount = 0;
                            int count = 0;
                            int loaded = 0;
                            var extract = new T();
                            string action = extract.GetAddAction();
                            

                            while (reader.Read())
                            {

                                totalcount++;
                                ShowPercentage(totalcount);
                                count++;


                                extract = new T();
                                await extract.LoadAsync(reader);

                                //loaded++;

                                if (_batchSize == 0)
                                {
                                    try
                                    {
                                        if (_connection.State != ConnectionState.Open)
                                        {
                                            await _connection.OpenAsync();
                                        }
                                        var tx = _connection.BeginTransaction(IsolationLevel.RepeatableRead);
                                        loaded += await _connection.ExecuteAsync(action, extract, tx, 0);
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

                                    if (count == _batchSize && _batchSize > 0)
                                    {
                                        try
                                        {
                                            if (_connection.State != ConnectionState.Open)
                                            {
                                                await _connection.OpenAsync();
                                            }
                                            var tx = _connection.BeginTransaction(IsolationLevel.RepeatableRead);
                                            loaded += await _connection.ExecuteAsync(action, extracts, tx, 0);
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
                                    if (_connection.State != ConnectionState.Open)
                                    {
                                        await _connection.OpenAsync();
                                    }
                                    var tx = _connection.BeginTransaction(IsolationLevel.RepeatableRead);
                                    loaded += await _connection.ExecuteAsync(action, extracts, tx, 0);
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

                            currentHistory = _emrRepository.GetStats(_extractSetting.Id);

                            _summary.Loaded = currentHistory.Loaded ?? loaded;
                            _summary.Total = currentHistory.Found ?? totalcount;
                        }
                    }

                }

            }
            return _summary;
        }

        private void ShowPercentage(int progress)
        {
            if (null == _progress)
                return;
            _processStatus.Progress = progress;
            _processStatus.ExtractSetting = _extractSetting;
            _progress.Report(_processStatus);
        }

        private Task<IDataReader> GetTask(IDbCommand command)
        {
            return Task.Run(() => command.ExecuteReader());
        }

        
    }


}
