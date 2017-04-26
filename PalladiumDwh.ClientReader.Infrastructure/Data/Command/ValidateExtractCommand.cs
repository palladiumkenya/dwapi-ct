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

            var emr = _emrRepository.GetDefault();

            if (null == emr) throw new Exception($"No Default EMR Setup !");

            _extractSetting = emr.GetActiveExtractSetting($"{extractName}");

            if (null == _extractSetting) throw new Exception($"No Extract Setting found for {emr}");

            commandText = _extractSetting.ExtractSql;

            if (string.IsNullOrWhiteSpace(commandText)) throw new Exception($"No sql command found for {extractName}");

            

            _validators = _validatorRepository.GetByExtract(extractName).ToList();

            ShowPercentage(1,1);

            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                int totalcount = _validators.Count;
                int count = 0;
                int errorCount = 0;

                foreach (var v in _validators)
                {
                    using (var command = _connection.CreateCommand())
                    {
                        command.CommandText = v.GenerateValidateSql();

                        try
                        {
                            errorCount += await GetTask(command);
                        }
                        catch (Exception e)
                        {
                            Log.Debug(e);
                            throw;
                        }

                        count++;
                        ShowPercentage(count,totalcount);
                    }
                }

                _summary.Total = await GetNumberOfRecordsWithErrors(_connection.CreateCommand(), extractName);
            }
            return _summary;
        }
        private void ShowPercentage(double count,double total)
        {
            var perc = (count / total) * 100;
            if (null == _progress)
                return;
            _processStatus.Progress = (int) perc;

            _processStatus.ExtractSetting = _extractSetting;
            _progress.Report(_processStatus);
        }

        private Task<int> GetTask(IDbCommand command)
        {
            return Task.Run(() => command.ExecuteNonQuery());
        }
        private Task<int> GetNumberOfRecordsWithErrors(IDbCommand command,string extractName)
        {
            command.CommandText = $@"
                SELECT count(Id)
                FROM {extractName}
                where CheckError=1";

            return Task.Run(() => Convert.ToInt32(command.ExecuteScalar()));
        }
    }


}
