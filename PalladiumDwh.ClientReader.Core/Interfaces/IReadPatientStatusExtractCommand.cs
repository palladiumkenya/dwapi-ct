using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReadPatientStatusExtractCommand : IReadCommand<PatientStatusExtractRow>
    {
    }
}