﻿using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class SyncCsvService : ISyncCsvService
    {
        private readonly IClearCsvExtractsCommand _clearCsvExtractsCommand;

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

        public SyncCsvService(IClearCsvExtractsCommand clearCsvExtractsCommand,
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
            _clearCsvExtractsCommand = clearCsvExtractsCommand;

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

        public async Task<int> InitializeAsync()
        {
          return   await _clearCsvExtractsCommand.ExecuteAsync();
        }

        public async Task<RunSummary> SyncAsync(ExtractSetting extract, string extractCsv, Progress<ProcessStatus> progressPercent = null,Progress < DProgress > progress = null)
        {

            var summary = new RunSummary {ExtractSetting = extract};

            if (extract.Destination == nameof(TempPatientExtract))
            {
                summary.LoadSummary = await _loadPatientExtractCsvCommand.ExecuteAsync(extractCsv, progress);
                summary.ValidationSummary = await _validatePatientExtractCommand.ExecuteValidateAsync(progress);
                summary.SyncSummary = await _syncPatientExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientArtExtract))
            {
                summary.LoadSummary = await _loadPatientArtExtractCsvCommand.ExecuteAsync(extractCsv, progress);
                summary.ValidationSummary = await _validatePatientArtExtractCommand.ExecuteValidateAsync(progress);
                summary.SyncSummary = await _syncPatientArtExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientBaselinesExtract))
            {
                summary.LoadSummary = await _loadPatientBaselinesExtractCsvCommand.ExecuteAsync(extractCsv, progress);
                summary.ValidationSummary = await _validatePatientBaselinesExtractCommand.ExecuteValidateAsync(progress);
                summary.SyncSummary = await _syncPatientBaselinesExtractCommand.ExecuteAsync();
            }

            if (extract.Destination == nameof(TempPatientStatusExtract))
            {
                summary.LoadSummary = await _loadPatientStatusExtractCsvCommand.ExecuteAsync(extractCsv, progress);
                summary.ValidationSummary = await _validatePatientStatusExtractCommand.ExecuteValidateAsync(progress);
                summary.SyncSummary = await _syncPatientStatusExtractCommand.ExecuteAsync();

            }


            if (extract.Destination == nameof(TempPatientVisitExtract))
            {
                summary.LoadSummary = await _loadPatientVisitExtractCsvCommand.ExecuteAsync(extractCsv, progress);
                summary.ValidationSummary = await _validatePatientVisitExtractCommand.ExecuteValidateAsync(progress);
                summary.SyncSummary = await _syncPatientVisitExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientLaboratoryExtract))
            {
                summary.LoadSummary = await _loadPatientLaboratoryExtractCsvCommand.ExecuteAsync(extractCsv, progress);
                summary.ValidationSummary =
                    await _validatePatientLaboratoryExtractCommand.ExecuteValidateAsync(progress);
                summary.SyncSummary = await _syncPatientLaboratoryExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientPharmacyExtract))
            {
                summary.LoadSummary = await _loadPatientPharmacyExtractCsvCommand.ExecuteAsync(extractCsv, progress);
                summary.ValidationSummary = await _validatePatientPharmacyExtractCommand.ExecuteValidateAsync(progress);
                summary.SyncSummary = await _syncPatientPharmacyExtractCommand.ExecuteAsync();
            }

            return summary;
        }
    }
}