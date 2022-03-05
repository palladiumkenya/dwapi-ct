namespace PalladiumDwh.Shared.Enum
{
    public enum CargoType
    {
        Patient,
        Metrics,
        AppMetrics,
        Migration,
        Indicator
    }

    public enum EmrSetup
    {
        SingleFacility,
        MultiFacility,
        Community
    }

    public enum UploadMode
    {
        Normal,
        Differential,
        Smart
    }

    public enum LiveStage
    {
        Rest,
        Assigned,
        Merged
    }
}
