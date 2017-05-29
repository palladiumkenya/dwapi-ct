using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core.Interfaces
{
    public interface IProfileManager
    {
        ClientPatientExtract Decode(string encodedPatient);
        IEnumerable<IClientExtractProfile> Generate(ClientPatientExtract patient);
    }
}