using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;
using Dapper;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class LoadExtractCommand<T> : ILoadExtractCommand<T> where T : TempExtract
    {

        private readonly IEMRRepository _emrRepository;
        private readonly IDbConnection _connection;
        private readonly IDbConnection _emrConnection;
        private readonly int _batchSize;
        private ExtractSetting _extractSetting;
        private LoadSummary _summary;

        public LoadExtractCommand(IEMRRepository emrRepository, int batchSize = 100)
        {
            _emrRepository = emrRepository;
            _batchSize = batchSize;
            _connection = _emrRepository.GetConnection();
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
                            var extract = (T) Activator.CreateInstance(typeof(T));
                            string action = extract.GetAddAction();

                            while (reader.Read())
                            {
                                totalcount++;
                                count++;
                                
                                extract = (T) Activator.CreateInstance(typeof(T));
                                extract.Load(reader);
                                if (!extract.HasError)
                                {
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

        public virtual async Task<LoadSummary> ExecuteAsync()
        {
            return await Task.Run(() =>
            {
                Execute();
                return Summary;
            });
        }
    }
}
