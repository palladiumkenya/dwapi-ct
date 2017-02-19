﻿using System;
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
                        TRUNCATE TABLE TempPatientArtExtract;TRUNCATE TABLE PatientArtExtract;
                        TRUNCATE TABLE TempPatientBaselinesExtract;TRUNCATE TABLE PatientBaselinesExtract;
                        TRUNCATE TABLE TempPatientLaboratoryExtract;TRUNCATE TABLE PatientLaboratoryExtract;
                        TRUNCATE TABLE TempPatientPharmacyExtract;TRUNCATE TABLE PatientPharmacyExtract;
                        TRUNCATE TABLE TempPatientVisitExtract;TRUNCATE TABLE PatientVisitExtract;
                        TRUNCATE TABLE TempPatientStatusExtract;TRUNCATE TABLE PatientStatusExtract;
                        TRUNCATE TABLE TempPatientExtract;
                        DELETE FROM PatientExtract;
                                ";
                    totalCount = command.ExecuteNonQuery();
                }
            }
            return totalCount;
        }

        private Task<int> TruncateCommand(string extract)
        {
            _progressValue++;
            var count = GetCommand(extract, "TRUNCATE TABLE").ExecuteNonQueryAsync();
            ShowPercentage(_progressValue);
            return count;
        }

        private Task<int> DeleteCommand(string extract)
        {
            _progressValue++;
            var count = GetCommand(extract, "DELETE FROM").ExecuteNonQueryAsync();
            ShowPercentage(_progressValue);
            return count;
        }

        private SqlCommand GetCommand(string extract, string action)
        {
            var command = _connection.CreateCommand();
            command.CommandTimeout = 0;
            command.CommandText = $@" {action} {extract}; ";
            return command;
        }

        public async Task<int> ExecuteAsync()
        {
            _progressValue = 1;

            int totalCount;

            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
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
            if (null == _progress)
                return;
            decimal status = (progress / _taskCount) * 100;
            _progress.Report((int) status);
        }
    }
}