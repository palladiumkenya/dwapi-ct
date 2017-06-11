using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IImportCsvService
    {
        Task<List<string>> CopyCsvFilesAsync(List<string> files, string csvImportDir = "", IProgress<DProgress> progress = null);
        Task<IEnumerable<string>> ReadCsvFilesAsync(string csvImportDir = "", IProgress<DProgress> progress = null);
        string GetExtractByFileName(string filename);
    }
}