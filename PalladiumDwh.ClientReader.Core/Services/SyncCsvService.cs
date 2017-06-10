using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class SyncCsvService : ISyncCsvService
    {
        private readonly IClearExtractsCommand _clearExtractsCommand;

        private readonly ILoadPatientExtractCsvCommand _loadPatientExtractCsvCommand;
        private readonly ILoadPatientArtExtractCsvCommand _loadPatientArtExtractCsvCommand;
        private readonly ILoadPatientBaselinesExtractCsvCommand _loadPatientBaselinesExtractCsvCommand;
        private readonly ILoadPatientLaboratoryExtractCsvCommand _loadPatientLaboratoryExtractCsvCommand;
        private readonly ILoadPatientPharmacyExtractCsvCommand _loadPatientPharmacyExtractCsvCommand;
        private readonly ILoadPatientStatusExtractCsvCommand _loadPatientStatusExtractCsvCommand;
        private readonly ILoadPatientVisitExtractCsvCommand _loadPatientVisitExtractCsvCommand;

        private readonly IValidatePatientExtractCommand _validatePatientExtractCommand;
        private readonly IValidatePatientArtExtractCommand _validatePatientArtExtractCommand;
        private readonly IValidatePatientBaselinesExtractCommand _validatePatientBaselinesExtractCommand;
        private readonly IValidatePatientLaboratoryExtractCommand _validatePatientLaboratoryExtractCommand;
        private readonly IValidatePatientPharmacyExtractCommand _validatePatientPharmacyExtractCommand;
        private readonly IValidatePatientStatusExtractCommand _validatePatientStatusExtractCommand;
        private readonly IValidatePatientVisitExtractCommand _validatePatientVisitExtractCommand;

        private readonly ISyncPatientExtractCommand _syncPatientExtractCommand;
        private readonly ISyncPatientArtExtractCommand _syncPatientArtExtractCommand;
        private readonly ISyncPatientBaselinesExtractCommand _syncPatientBaselinesExtractCommand;
        private readonly ISyncPatientLaboratoryExtractCommand _syncPatientLaboratoryExtractCommand;
        private readonly ISyncPatientPharmacyExtractCommand _syncPatientPharmacyExtractCommand;
        private readonly ISyncPatientVisitExtractCommand _syncPatientVisitExtractCommand;
        private readonly ISyncPatientStatusExtractCommand _syncPatientStatusExtractCommand;

        private SyncSummary _summary;

        public SyncCsvService(IClearExtractsCommand clearExtractsCommand,
            ILoadPatientExtractCsvCommand loadPatientExtractCsvCommand,
            ILoadPatientArtExtractCsvCommand loadPatientArtExtractCsvCommand,
            ILoadPatientBaselinesExtractCsvCommand loadPatientBaselinesExtractCsvCommand,
            ILoadPatientLaboratoryExtractCsvCommand loadPatientLaboratoryExtractCsvCommand,
            ILoadPatientPharmacyExtractCsvCommand loadPatientPharmacyExtractCsvCommand,
            ILoadPatientStatusExtractCsvCommand loadPatientStatusExtractCsvCommand,
            ILoadPatientVisitExtractCsvCommand loadPatientVisitExtractCsvCommand,
            IValidatePatientExtractCommand validatePatientExtractCommand,
            IValidatePatientArtExtractCommand validatePatientArtExtractCommand,
            IValidatePatientBaselinesExtractCommand validatePatientBaselinesExtractCommand,
            IValidatePatientLaboratoryExtractCommand validatePatientLaboratoryExtractCommand,
            IValidatePatientPharmacyExtractCommand validatePatientPharmacyExtractCommand,
            IValidatePatientStatusExtractCommand validatePatientStatusExtractCommand,
            IValidatePatientVisitExtractCommand validatePatientVisitExtractCommand,
            ISyncPatientExtractCommand syncPatientExtractCommand,
            ISyncPatientArtExtractCommand syncPatientArtExtractCommand,
            ISyncPatientBaselinesExtractCommand syncPatientBaselinesExtractCommand,
            ISyncPatientLaboratoryExtractCommand syncPatientLaboratoryExtractCommand,
            ISyncPatientPharmacyExtractCommand syncPatientPharmacyExtractCommand,
            ISyncPatientVisitExtractCommand syncPatientVisitExtractCommand,
            ISyncPatientStatusExtractCommand syncPatientStatusExtractCommand)
        {
            _clearExtractsCommand = clearExtractsCommand;

            _loadPatientExtractCsvCommand = loadPatientExtractCsvCommand;
            _loadPatientArtExtractCsvCommand = loadPatientArtExtractCsvCommand;
            _loadPatientBaselinesExtractCsvCommand = loadPatientBaselinesExtractCsvCommand;
            _loadPatientLaboratoryExtractCsvCommand = loadPatientLaboratoryExtractCsvCommand;
            _loadPatientPharmacyExtractCsvCommand = loadPatientPharmacyExtractCsvCommand;
            _loadPatientStatusExtractCsvCommand = loadPatientStatusExtractCsvCommand;
            _loadPatientVisitExtractCsvCommand = loadPatientVisitExtractCsvCommand;

            _validatePatientExtractCommand = validatePatientExtractCommand;
            _validatePatientArtExtractCommand = validatePatientArtExtractCommand;
            _validatePatientBaselinesExtractCommand = validatePatientBaselinesExtractCommand;
            _validatePatientLaboratoryExtractCommand = validatePatientLaboratoryExtractCommand;
            _validatePatientPharmacyExtractCommand = validatePatientPharmacyExtractCommand;
            _validatePatientStatusExtractCommand = validatePatientStatusExtractCommand;
            _validatePatientVisitExtractCommand = validatePatientVisitExtractCommand;

            _syncPatientExtractCommand = syncPatientExtractCommand;
            _syncPatientArtExtractCommand = syncPatientArtExtractCommand;
            _syncPatientBaselinesExtractCommand = syncPatientBaselinesExtractCommand;
            _syncPatientLaboratoryExtractCommand = syncPatientLaboratoryExtractCommand;
            _syncPatientPharmacyExtractCommand = syncPatientPharmacyExtractCommand;
            _syncPatientVisitExtractCommand = syncPatientVisitExtractCommand;
            _syncPatientStatusExtractCommand = syncPatientStatusExtractCommand;
        }

        public void Initialize()
        {
            _clearExtractsCommand.Execute();
        }

        public async Task InitializeAsync()
        {
            await _clearExtractsCommand.ExecuteAsync();
        }

        public RunSummary Sync(ExtractSetting extract)
        {
            var summary = new RunSummary {ExtractSetting = extract};

            if (extract.Destination == nameof(TempPatientExtract))
            {
                _loadPatientExtractCsvCommand.Execute();
                summary.LoadSummary = _loadPatientExtractCsvCommand.Summary;
                _syncPatientExtractCommand.Execute();
                summary.SyncSummary = _syncPatientExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientArtExtract))
            {
                _loadPatientArtExtractCsvCommand.Execute();
                summary.LoadSummary = _loadPatientArtExtractCsvCommand.Summary;
                _syncPatientArtExtractCommand.Execute();
                summary.SyncSummary = _syncPatientArtExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientBaselinesExtract))
            {
                _loadPatientBaselinesExtractCsvCommand.Execute();
                summary.LoadSummary = _loadPatientBaselinesExtractCsvCommand.Summary;
                _syncPatientBaselinesExtractCommand.Execute();
                summary.SyncSummary = _syncPatientBaselinesExtractCommand.Summary;
            }

            if (extract.Destination == nameof(TempPatientStatusExtract))
            {
                _loadPatientStatusExtractCsvCommand.Execute();
                summary.LoadSummary = _loadPatientStatusExtractCsvCommand.Summary;
                _syncPatientStatusExtractCommand.Execute();
                summary.SyncSummary = _syncPatientStatusExtractCommand.Summary;
            }




            if (extract.Destination == nameof(TempPatientVisitExtract))
            {
                _loadPatientVisitExtractCsvCommand.Execute();
                summary.LoadSummary = _loadPatientVisitExtractCsvCommand.Summary;
                _syncPatientVisitExtractCommand.Execute();
                summary.SyncSummary = _syncPatientVisitExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientLaboratoryExtract))
            {
                _loadPatientLaboratoryExtractCsvCommand.Execute();
                summary.LoadSummary = _loadPatientLaboratoryExtractCsvCommand.Summary;
                _syncPatientLaboratoryExtractCommand.Execute();
                summary.SyncSummary = _syncPatientLaboratoryExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientPharmacyExtract))
            {
                _loadPatientPharmacyExtractCsvCommand.Execute();
                summary.LoadSummary = _loadPatientPharmacyExtractCsvCommand.Summary;
                _syncPatientPharmacyExtractCommand.Execute();
                summary.SyncSummary = _syncPatientPharmacyExtractCommand.Summary;
            }

            return summary;
        }

        public async Task<RunSummary> SyncAsync(ExtractSetting extract, Progress<ProcessStatus> progressPercent = null)
        {

            var summary = new RunSummary {ExtractSetting = extract};

            if (extract.Destination == nameof(TempPatientExtract))
            {
                summary.LoadSummary = await _loadPatientExtractCsvCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientArtExtract))
            {
                summary.LoadSummary = await _loadPatientArtExtractCsvCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientArtExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientArtExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientBaselinesExtract))
            {
                summary.LoadSummary = await _loadPatientBaselinesExtractCsvCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientBaselinesExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientBaselinesExtractCommand.ExecuteAsync();
            }

            if (extract.Destination == nameof(TempPatientStatusExtract))
            {
                summary.LoadSummary = await _loadPatientStatusExtractCsvCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientStatusExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientStatusExtractCommand.ExecuteAsync();

            }


            if (extract.Destination == nameof(TempPatientVisitExtract))
            {
                summary.LoadSummary = await _loadPatientVisitExtractCsvCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientVisitExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientVisitExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientLaboratoryExtract))
            {
                summary.LoadSummary = await _loadPatientLaboratoryExtractCsvCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary =
                    await _validatePatientLaboratoryExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientLaboratoryExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientPharmacyExtract))
            {
                summary.LoadSummary = await _loadPatientPharmacyExtractCsvCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientPharmacyExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientPharmacyExtractCommand.ExecuteAsync();
            }

            return summary;
        }

        public void SyncAll()
        {
            _clearExtractsCommand.Execute();
            SyncPatients();
            SynPatientsArt();
            SynPatientsBaselines();
            SynPatientsLab();
            SynPatientsPharmacy();
            SynPatientsVisits();
            SynPatientsStatus();

        }

        public void SyncPatients()
        {
            _loadPatientExtractCsvCommand.Execute();
            _syncPatientExtractCommand.Execute();
        }


        public void SynPatientsArt()
        {
            _loadPatientArtExtractCsvCommand.Execute();
            _syncPatientArtExtractCommand.Execute();
        }

        public void SynPatientsBaselines()
        {
            _loadPatientBaselinesExtractCsvCommand.Execute();
            _syncPatientBaselinesExtractCommand.Execute();
        }

        public void SynPatientsStatus()
        {
            _loadPatientStatusExtractCsvCommand.Execute();
            _syncPatientStatusExtractCommand.Execute();
        }

        public void SynPatientsPharmacy()
        {
            _loadPatientPharmacyExtractCsvCommand.Execute();
            _syncPatientPharmacyExtractCommand.Execute();
        }

        public void SynPatientsLab()
        {
            _loadPatientLaboratoryExtractCsvCommand.Execute();
            _syncPatientLaboratoryExtractCommand.Execute();
        }

        public void SynPatientsVisits()
        {
            _loadPatientVisitExtractCsvCommand.Execute();
            _syncPatientVisitExtractCommand.Execute();
        }

        public async Task<RunSummary> SyncPatientsAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientExtractCsvCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsArtAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientArtExtractCsvCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientArtExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientArtExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsBaselinesAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientBaselinesExtractCsvCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientBaselinesExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientBaselinesExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsStatusAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientStatusExtractCsvCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientStatusExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientStatusExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsPharmacyAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientPharmacyExtractCsvCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientPharmacyExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientPharmacyExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsLabAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientLaboratoryExtractCsvCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientLaboratoryExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientLaboratoryExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsVisitsAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientVisitExtractCsvCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientVisitExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientVisitExtractCommand.ExecuteAsync()
            };

            return summary;
        }
    }
}