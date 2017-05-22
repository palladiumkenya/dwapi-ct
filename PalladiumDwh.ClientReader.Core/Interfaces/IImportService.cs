using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IImportService
    {
        Task<IEnumerable<ClientPatientExtract>> ReadExportsAsync(string importDir);
        string Base64Decode(string base64EncodedData);
    }
}