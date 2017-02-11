using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class ClearExtractsCommand : IClearExtractsCommand
    {
        private readonly IEMRRepository emrRepository;
        private SqlConnection _connection;

        public ClearExtractsCommand(IEMRRepository emrRepository)
        {
            this.emrRepository = emrRepository;
            _connection = emrRepository.GetConnection() as SqlConnection;
        }

        public void Execute()
        {
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
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task ExecuteAsync()
        {
            using (_connection)
            {
                await _connection.OpenAsync();
                var command = _connection.CreateCommand();
                command.CommandTimeout = 0;
                Parallel.Invoke(
                    async () =>
                    {
                        command.CommandText = $"DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract;";
                        await command.ExecuteNonQueryAsync();
                    },
                    async () =>
                    {
                        command.CommandText =$"DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract;";
                        await command.ExecuteNonQueryAsync();
                    },
                    async () =>
                    {
                        command.CommandText = $"DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract;";
                        await command.ExecuteNonQueryAsync();
                    },
                    async () =>
                    {
                        command.CommandText =$"DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract;";
                        await command.ExecuteNonQueryAsync();
                    },
                    async () =>
                    {
                        command.CommandText =$"DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract;";
                        await command.ExecuteNonQueryAsync();
                    },
                    async () =>
                    {
                        command.CommandText = $"DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract;";
                        await command.ExecuteNonQueryAsync();
                    }
                );

                command.CommandText = $"DELETE FROM PatientExtract ";
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}