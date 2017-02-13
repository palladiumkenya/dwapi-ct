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

        public async Task<RunSummary> SyncAsync(ExtractSetting extract)
        {

            var summary = new RunSummary { ExtractSetting = extract };

            if (extract.Destination == nameof(TempPatientExtract))
            {
                summary.LoadSummary = await _loadPatientExtractCommand.ExecuteAsync();
                summary.SyncSummary = await _syncPatientExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientArtExtract))
            {
                summary.LoadSummary = await _loadPatientArtExtractCommand.ExecuteAsync();
                summary.SyncSummary = await _syncPatientArtExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientBaselinesExtract))
            {
                summary.LoadSummary = await _loadPatientBaselinesExtractCommand.ExecuteAsync();
                summary.SyncSummary = await _syncPatientBaselinesExtractCommand.ExecuteAsync();
            }

            if (extract.Destination == nameof(TempPatientStatusExtract))
            {
                summary.LoadSummary = await _loadPatientStatusExtractCommand.ExecuteAsync();
                summary.SyncSummary = await _syncPatientStatusExtractCommand.ExecuteAsync();

            }


            if (extract.Destination == nameof(TempPatientVisitExtract))
            {
                summary.LoadSummary = await _loadPatientVisitExtractCommand.ExecuteAsync();
                summary.SyncSummary = await _syncPatientVisitExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientLaboratoryExtract))
            {
                summary.LoadSummary = await _loadPatientLaboratoryExtractCommand.ExecuteAsync();
                summary.SyncSummary = await _syncPatientLaboratoryExtractCommand.ExecuteAsync();
            }


            if (extract.Destination == nameof(TempPatientPharmacyExtract))
            {
                summary.LoadSummary = await _loadPatientPharmacyExtractCommand.ExecuteAsync();
                summary.SyncSummary = await _syncPatientPharmacyExtractCommand.ExecuteAsync();
            }



            return summary;
        }

        public void SyncAll()
        {
            _clearExtractsCommand.Execute();
            SyncPatients();SynPatientsArt();SynPatientsBaselines();
            SynPatientsLab();SynPatientsPharmacy();SynPatientsVisits();
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
    }
}