using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IImportService
    {
        Task<List<ImportManifest>> GetCurrentImports(string importDir = "", IProgress<int> progress = null);
        Task<List<ImportManifest>> ExtractExportsAsync(List<string> exportFiles,string importDir="", IProgress<int> progress = null);
        Task<IEnumerable<ClientPatientExtract>> ReadExportsAsync(string importDir);
        string Base64Decode(string base64EncodedData);
    }
}