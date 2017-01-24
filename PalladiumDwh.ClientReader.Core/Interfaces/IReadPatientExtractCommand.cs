using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IReadPatientExtractCommand:IReadCommand<IPatientExtractRow>
    {
    }
}