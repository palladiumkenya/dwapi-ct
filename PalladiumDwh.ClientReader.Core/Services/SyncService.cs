using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class SyncService : ISyncService
    {
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

        public SyncService(ILoadPatientExtractCommand loadPatientExtractCommand,
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


        public void SyncAll()
        {
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