namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientExtractDTO
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        int FacilityId { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
    }
}