using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;
using Dapper;
using log4net;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class ValidateExtractCommand<T> : IValidateExtractCommand<T> where T : TempExtract, new()
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private readonly IEMRRepository _emrRepository;
        private readonly IValidatorRepository _validatorRepository;
        private readonly SqlConnection _connection;
        private readonly IDbConnection _emrConnection;
        private readonly int _batchSize;
        private ExtractSetting _extractSetting;
        private ValidationSummary _summary;
        private IProgress<ProcessStatus> _progress;
        private int recordCount = 0;
        ProcessStatus _processStatus=new ProcessStatus();
        private List<Validator> _validators;

        public ValidateExtractCommand(IEMRRepository emrRepository, IValidatorRepository validatorRepository, int batchSize = 2000)
        {
            _emrRepository = emrRepository;
            _validatorRepository = validatorRepository;
            _batchSize = batchSize;
            _connection = _emrRepository.GetConnection() as SqlConnection; 
            _emrConnection = _emrRepository.GetEmrConnection();
        }

        public ValidationSummary Summary => _summary;

        public virtual async Task<ValidationSummary> ExecuteAsync(Progress<ProcessStatus> progressPercent = null)
        {
            _progress = progressPercent;
            _summary = new ValidationSummary();

            var extractName = typeof(T).Name;
            var commandText = string.Empty;

            Log.Debug($"Executing Validate {extractName} command...");

            _validators = _validatorRepository.GetByExtract(extractName).ToList();

            ShowPercentage(1);

            using (_emrConnection)
            {
                if (_emrConnection.State != ConnectionState.Open)
                {
                    _emrConnection.Open();
                }

                foreach (var v in _validators)
                {
                    using (var command = _emrConnection.CreateCommand())
                    {
                        command.CommandText = v.GenerateValidateSQL();

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
                                string errorAction = extract.GetAddErrorAction();

                                while (reader.Read())
                                {

                                    totalcount++;
                                    ShowPercentage(totalcount);
                                    count++;


                                    extract = new T();
                                    await extract.LoadAsync(reader);

                                    if (!extract.HasError)
                                    {
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
                                    else
                                    {
                                        //TODO:Move to Error Summary
                                        try
                                        {
                                            if (_connection.State != ConnectionState.Open)
                                            {
                                                await _connection.OpenAsync();
                                            }
                                            var tx = _connection.BeginTransaction(IsolationLevel.RepeatableRead);
                                            await _connection.ExecuteAsync(errorAction, extract, tx, 0);
                                            tx.Commit();
                                        }
                                        catch (Exception e)
                                        {
                                            Log.Debug(e);
                                            throw;
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
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Debug(e);
                                        throw;
                                    }
                                }

                                //  _summary.Loaded = loaded;
                                _summary.Total = totalcount;
                            }
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
