using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IImportService
    {
        
        Task<List<ImportManifest>> ExtractExportsAsync(List<string> exportFiles,string importDir="", IProgress<DProgress> progress = null);
        Task<IEnumerable<SiteManifest>> ReadExportsAsync(string importDir = "", IProgress<DProgress> progress = null);

        Task<SiteManifest> GetSiteManifest(string importLocation, IProgress<DProgress> progress = null);
        Task ClearExportsAsync(List<string> exportFolders, IProgress<DProgress> progress = null);
        string Base64Decode(string base64EncodedData);

    }
}