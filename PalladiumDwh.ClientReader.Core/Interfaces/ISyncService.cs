namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncService
    {
        void SyncPatients();
        void SynPatientsArt();
        void SynPatientsBaselines();
        void SynPatientsStatus();
        void SynPatientsPharmacy();
        void SynPatientsLab();
        void SynPatientsVisits();
    }
}