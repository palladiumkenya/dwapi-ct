using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MySql.Data.MySqlClient.Authentication;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class ClearExtractsCommand : IClearExtractsCommand
    {
        private readonly IEMRRepository _emrRepository;
        private readonly SqlConnection _connection;
        private IProgress<int> _progress;
        private int _progressValue;
        private int _taskCount;

        public ClearExtractsCommand(IEMRRepository emrRepository)
        {
            _emrRepository = emrRepository;
            _connection = emrRepository.GetConnection() as SqlConnection;
        }

        public int Execute()
        {
            int totalCount;
            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"
                        DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract
                        DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract;
                        DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract;
                        DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract;
                        DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract;
                        DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract;
                        DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract;
                                ";
                    totalCount = command.ExecuteNonQuery();
                }
            }
            return totalCount;
        }

        private Task<int> TruncateCommand(string extract)
        {
            _progressValue++;
           var count= GetCommand(extract, "TRUNCATE TABLE").ExecuteNonQueryAsync();
            _progress.Report(_progressValue);
            return count;
        }

        private Task<int> DeleteCommand(string extract)
        {
            _progressValue++;
           var count=GetCommand(extract, "DELETE FROM").ExecuteNonQueryAsync();
            _progress.Report(_progressValue);
            return count;
        }

        private SqlCommand GetCommand(string extract, string action)
        {
            var command = _connection.CreateCommand();
            command.CommandTimeout = 0;
            command.CommandText = $@" {action} {extract}; ";
            return command;
        }

        public async Task<int> ExecuteAsync(IProgress<int> progress)
        {
            _progressValue = 1;
            _progress = progress;
            int totalCount;

            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                var command = _connection.CreateCommand();
                command.CommandTimeout = 0;

                var count = await Task.WhenAll(
                    TruncateCommand(nameof(TempPatientExtract)),
                    TruncateCommand(nameof(TempPatientArtExtract)), TruncateCommand(nameof(TempPatientArtExtract)),
                    TruncateCommand(nameof(TempPatientBaselinesExtract)),
                    TruncateCommand(nameof(PatientBaselinesExtract)),
                    TruncateCommand(nameof(TempPatientStatusExtract)), TruncateCommand(nameof(PatientStatusExtract)),
                    TruncateCommand(nameof(TempPatientLaboratoryExtract)),
                    TruncateCommand(nameof(PatientLaboratoryExtract)),
                    TruncateCommand(nameof(TempPatientPharmacyExtract)), TruncateCommand(nameof(PatientPharmacyExtract)),
                    TruncateCommand(nameof(TempPatientVisitExtract)), TruncateCommand(nameof(PatientVisitExtract))
                );

                
                totalCount = await DeleteCommand(nameof(PatientExtract));
            }

            return totalCount;
        }

        private void ShowPercentage(int progress)
        {
            decimal status = (progress/_taskCount)*100;
            _progress.Report((int) status);
        }
    }
}