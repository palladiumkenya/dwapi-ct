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
        MultiFacility
    }

    public enum UploadMode
    {
        Normal,
        Differential
    }
}
