using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class SyncService : ISyncService
    {
        private readonly IClearExtractsCommand _clearExtractsCommand;

        private readonly ILoadPatientExtractCommand _loadPatientExtractCommand;
        private readonly ILoadPatientArtExtractCommand _loadPatientArtExtractCommand;
        private readonly ILoadPatientBaselinesExtractCommand _loadPatientBaselinesExtractCommand;
        private readonly ILoadPatientLaboratoryExtractCommand _loadPatientLaboratoryExtractCommand;
        private readonly ILoadPatientPharmacyExtractCommand _loadPatientPharmacyExtractCommand;
        private readonly ILoadPatientStatusExtractCommand _loadPatientStatusExtractCommand;
        private readonly ILoadPatientVisitExtractCommand _loadPatientVisitExtractCommand;

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

        public SyncService(IClearExtractsCommand clearExtractsCommand,
            ILoadPatientExtractCommand loadPatientExtractCommand,
            ILoadPatientArtExtractCommand loadPatientArtExtractCommand,
            ILoadPatientBaselinesExtractCommand loadPatientBaselinesExtractCommand,
            ILoadPatientLaboratoryExtractCommand loadPatientLaboratoryExtractCommand,
            ILoadPatientPharmacyExtractCommand loadPatientPharmacyExtractCommand,
            ILoadPatientStatusExtractCommand loadPatientStatusExtractCommand,
            ILoadPatientVisitExtractCommand loadPatientVisitExtractCommand,
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

            _loadPatientExtractCommand = loadPatientExtractCommand;
            _loadPatientArtExtractCommand = loadPatientArtExtractCommand;
            _loadPatientBaselinesExtractCommand = loadPatientBaselinesExtractCommand;
            _loadPatientLaboratoryExtractCommand = loadPatientLaboratoryExtractCommand;
            _loadPatientPharmacyExtractCommand = loadPatientPharmacyExtractCommand;
            _loadPatientStatusExtractCommand = loadPatientStatusExtractCommand;
            _loadPatientVisitExtractCommand = loadPatientVisitExtractCommand;

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
                _loadPatientExtractCommand.Execute();
                summary.LoadSummary = _loadPatientExtractCommand.Summary;
                _syncPatientExtractCommand.Execute();
                summary.SyncSummary = _syncPatientExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientArtExtract))
            {
                _loadPatientArtExtractCommand.Execute();
                summary.LoadSummary = _loadPatientArtExtractCommand.Summary;
                _syncPatientArtExtractCommand.Execute();
                summary.SyncSummary = _syncPatientArtExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientBaselinesExtract))
            {
                _loadPatientBaselinesExtractCommand.Execute();
                summary.LoadSummary = _loadPatientBaselinesExtractCommand.Summary;
                _syncPatientBaselinesExtractCommand.Execute();
                summary.SyncSummary = _syncPatientBaselinesExtractCommand.Summary;
            }

            if (extract.Destination == nameof(TempPatientStatusExtract))
            {
                _loadPatientStatusExtractCommand.Execute();
                summary.LoadSummary = _loadPatientStatusExtractCommand.Summary;
                _syncPatientStatusExtractCommand.Execute();
                summary.SyncSummary = _syncPatientStatusExtractCommand.Summary;
            }




            if (extract.Destination == nameof(TempPatientVisitExtract))
            {
                _loadPatientVisitExtractCommand.Execute();
                summary.LoadSummary = _loadPatientVisitExtractCommand.Summary;
                _syncPatientVisitExtractCommand.Execute();
                summary.SyncSummary = _syncPatientVisitExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientLaboratoryExtract))
            {
                _loadPatientLaboratoryExtractCommand.Execute();
                summary.LoadSummary = _loadPatientLaboratoryExtractCommand.Summary;
                _syncPatientLaboratoryExtractCommand.Execute();
                summary.SyncSummary = _syncPatientLaboratoryExtractCommand.Summary;
            }


            if (extract.Destination == nameof(TempPatientPharmacyExtract))
            {
                _loadPatientPharmacyExtractCommand.Execute();
                summary.LoadSummary = _loadPatientPharmacyExtractCommand.Summary;
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
                summary.LoadSummary = await _loadPatientExtractCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientArtExtract))
            {
                summary.LoadSummary = await _loadPatientArtExtractCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientArtExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientArtExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientBaselinesExtract))
            {
                summary.LoadSummary = await _loadPatientBaselinesExtractCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientBaselinesExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientBaselinesExtractCommand.ExecuteAsync();
            }

            if (extract.Destination == nameof(TempPatientStatusExtract))
            {
                summary.LoadSummary = await _loadPatientStatusExtractCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientStatusExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientStatusExtractCommand.ExecuteAsync();

            }


            if (extract.Destination == nameof(TempPatientVisitExtract))
            {
                summary.LoadSummary = await _loadPatientVisitExtractCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary = await _validatePatientVisitExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientVisitExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientLaboratoryExtract))
            {
                summary.LoadSummary = await _loadPatientLaboratoryExtractCommand.ExecuteAsync(progressPercent);
                summary.ValidationSummary =
                    await _validatePatientLaboratoryExtractCommand.ExecuteAsync(progressPercent);
                summary.SyncSummary = await _syncPatientLaboratoryExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientPharmacyExtract))
            {
                summary.LoadSummary = await _loadPatientPharmacyExtractCommand.ExecuteAsync(progressPercent);
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
            _loadPatientExtractCommand.Execute();
            _syncPatientExtractCommand.Execute();
        }


        public void SynPatientsArt()
        {
            _loadPatientArtExtractCommand.Execute();
            _syncPatientArtExtractCommand.Execute();
        }

        public void SynPatientsBaselines()
        {
            _loadPatientBaselinesExtractCommand.Execute();
            _syncPatientBaselinesExtractCommand.Execute();
        }

        public void SynPatientsStatus()
        {
            _loadPatientStatusExtractCommand.Execute();
            _syncPatientStatusExtractCommand.Execute();
        }

        public void SynPatientsPharmacy()
        {
            _loadPatientPharmacyExtractCommand.Execute();
            _syncPatientPharmacyExtractCommand.Execute();
        }

        public void SynPatientsLab()
        {
            _loadPatientLaboratoryExtractCommand.Execute();
            _syncPatientLaboratoryExtractCommand.Execute();
        }

        public void SynPatientsVisits()
        {
            _loadPatientVisitExtractCommand.Execute();
            _syncPatientVisitExtractCommand.Execute();
        }

        public async Task<RunSummary> SyncPatientsAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientExtractCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsArtAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientArtExtractCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientArtExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientArtExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsBaselinesAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientBaselinesExtractCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientBaselinesExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientBaselinesExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsStatusAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientStatusExtractCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientStatusExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientStatusExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsPharmacyAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientPharmacyExtractCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientPharmacyExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientPharmacyExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsLabAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientLaboratoryExtractCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientLaboratoryExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientLaboratoryExtractCommand.ExecuteAsync()
            };

            return summary;
        }

        public async Task<RunSummary> SynPatientsVisitsAsync()
        {
            var summary = new RunSummary
            {
                LoadSummary = await _loadPatientVisitExtractCommand.ExecuteAsync(),
                ValidationSummary = await _validatePatientVisitExtractCommand.ExecuteAsync(),
                SyncSummary = await _syncPatientVisitExtractCommand.ExecuteAsync()
            };

            return summary;
        }
    }
}