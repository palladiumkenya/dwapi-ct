using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
    public class ClearCsvExtractsCommand : IClearCsvExtractsCommand
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly SqlConnection _connection;

        private IProgress<DProgress> _dprogress;
        private int _dprogressValue;
        private int _taskCount;
        private string progressStatus = "Clearing existng Extracts";

        public ClearCsvExtractsCommand(IEMRRepository emrRepository)
        {
            _connection = emrRepository.GetConnection() as SqlConnection;
        }

        private Task<int> TruncateCommand(string extract)
        {
            _dprogressValue++;
            _dprogress?.ReportStatus($"{progressStatus}", _dprogressValue, _taskCount);
            var command = GetCommand(extract, "TRUNCATE TABLE");
            return command.ExecuteNonQueryAsync();
        }

        private Task<int> DeleteCommand(string extract)
        {
            _dprogressValue++;
            _dprogress?.ReportStatus($"{progressStatus}", _dprogressValue, _taskCount);
            var command = GetCommand(extract, "DELETE FROM");
            return command.ExecuteNonQueryAsync();
        }

        private SqlCommand GetCommand(string extract, string action)
        {
            var command = _connection.CreateCommand();
            command.CommandTimeout = 0;
            command.CommandText = $@" {action} {extract}; ";
            return command;
        }

        public async Task<int> ExecuteAsync(IProgress<DProgress> dprogress = null)
        {
            _dprogress = dprogress;

            _dprogress?.ReportStatus($"{progressStatus}...");

            Log.Debug($"Executing ClearExtracts command...");

            int totalCount = 0;


            var truncates = new List<string>
            {  nameof(EventHistory),
                nameof(TempPatientExtract),
                nameof(TempPatientArtExtract),
                nameof(PatientArtExtract),
                nameof(TempPatientBaselinesExtract),
                nameof(PatientBaselinesExtract),
                nameof(TempPatientStatusExtract),
                nameof(PatientStatusExtract),
                nameof(TempPatientLaboratoryExtract),
                nameof(PatientLaboratoryExtract),
                nameof(TempPatientPharmacyExtract),
                nameof(PatientPharmacyExtract),
                nameof(TempPatientVisitExtract),
                nameof(PatientVisitExtract)
            };

            var deletes = new List<string> { nameof(PatientExtract) };

            _taskCount = truncates.Count + deletes.Count + 1;

            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                var command = _connection.CreateCommand();
                command.CommandTimeout = 0;

                var parallelTasks = new List<Task<int>>();

                foreach (var name in truncates)
                {
                    parallelTasks.Add(TruncateCommand(name));
                }

                var orderdTasks = new List<Task<int>>();

                foreach (var name in deletes)
                {
                    orderdTasks.Add(DeleteCommand(name));
                }

                var count = await Task.WhenAll(parallelTasks);

                foreach (var t in orderdTasks)
                {
                    totalCount += await t;
                }

                _dprogress?.ReportStatus($"{progressStatus} Finished", _taskCount, _taskCount);
            }
            return totalCount;
        }
    }
}
}