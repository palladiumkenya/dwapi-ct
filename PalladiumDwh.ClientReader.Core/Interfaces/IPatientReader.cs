using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PatientExtractDTO = PalladiumDwh.ClientReader.Core.Model.PatientExtractDTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IPatientReader :IReader<PatientExtractDTO>
    {
        
    }
}