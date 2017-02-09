using System;
using System.Data;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class ClearExtractsCommand : IClearExtractsCommand
    {
        private readonly IEMRRepository emrRepository;
        private IDbConnection _connection;

        public ClearExtractsCommand(IEMRRepository emrRepository)
        {
            this.emrRepository = emrRepository;
            _connection = emrRepository.GetConnection();
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

        public virtual async Task ExecuteAsync()
        {
             await Task.Run(() =>
            {
                Execute();
            });
        }
    }
}