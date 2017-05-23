using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class ImportService : IImportService
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IClientPatientExtractRepository _clientPatientExtractRepository;
        private IProgress<int> _progress;
        private int _progressValue;
        private int _taskCount;

        public ImportService(IClientPatientExtractRepository clientPatientExtractRepository)
        {
            _clientPatientExtractRepository = clientPatientExtractRepository;
        }

        public async Task<List<ImportManifest>> GetCurrentImports(string importDir = "", IProgress<int> progress = null)
        {
            _progress = progress;
            var importManifests = new List<ImportManifest>();
            string folderToSaveTo;
            if (string.IsNullOrWhiteSpace(importDir))
            {
                //save to My Documents
                folderToSaveTo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                folderToSaveTo = importDir;
            }
            folderToSaveTo = folderToSaveTo.HasToEndsWith(@"\");
            string parentFolder = $@"{folderToSaveTo}DWapi\Imports\".HasToEndsWith(@"\");
            var dirs = Directory.GetDirectories(parentFolder).ToList();
            _taskCount = dirs.Count;
            try
            {
                foreach (var dir in dirs)
                {
                    await Task.Run(() =>
                    {
                        importManifests.Add(ImportManifest.Create(dir));
                    });

                    _progressValue++;
                    ShowPercentage(_progressValue);
                }
            }
            catch (Exception e)
            {
                Log.Debug(e);
            }

            return importManifests;
        }

        public async Task<List<ImportManifest>> ExtractExportsAsync(List<string> exportFiles, string importDir = "",
            IProgress<int> progress = null)
        {
            var importManifests = new List<ImportManifest>();

            _progress = progress;

            string parentFolder = "";
            string folderToSaveTo = "";
            _taskCount = exportFiles.Count;

            if (string.IsNullOrWhiteSpace(importDir))
            {
                //save to My Documents
                folderToSaveTo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                folderToSaveTo = importDir;
            }
            folderToSaveTo = folderToSaveTo.HasToEndsWith(@"\");
            parentFolder = $@"{folderToSaveTo}DWapi\Imports\".HasToEndsWith(@"\");

            bool exists = Directory.Exists(parentFolder);

            if (!exists)
            {
                Directory.CreateDirectory(parentFolder);
            }

            //unzip extract

            foreach (var f in exportFiles)
            {
                var folder = await UnZipExtracts(f, parentFolder);

                try
                {
                    await Task.Run(() =>
                    {
                        importManifests.Add(ImportManifest.Create(folder));
                    });
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                }

                _progressValue++;
                ShowPercentage(_progressValue);

            }

            return importManifests;
        }

        public async Task<IEnumerable<ClientPatientExtract>> ReadExportsAsync(string importDir)
        {
            List<ClientPatientExtract> decodedList=new List<ClientPatientExtract>();

            string folderToImportFrom = importDir;
            folderToImportFrom = folderToImportFrom.EndsWith("\\") ? folderToImportFrom : $"{folderToImportFrom}\\";

            bool exists = Directory.Exists(folderToImportFrom);

            if (!exists)
                throw new ArgumentException($"Folder {folderToImportFrom} doesnt exist");

            var files = await Task.Run(() => Directory.GetFiles(folderToImportFrom, "*.dwh", SearchOption.AllDirectories));

            foreach (var f in files)
            {
                var decoded = await Task.Run(() =>
                {
                    var content = File.ReadAllText(f);
                    return Base64Decode(content);
                });
                decodedList.Add(JsonConvert.DeserializeObject<ClientPatientExtract>(decoded));
            }

            return decodedList;
        }

        public  string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private Task<string> UnZipExtracts(string zipfile,string folder)
        {
            folder = $"{folder.HasToEndsWith(@"\")}{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}";
            folder = folder.HasToEndsWith(@"\");

            if (!File.Exists(zipfile))
                throw new ArgumentException($"File not found {zipfile}");

            return Task.Run(() =>
                {
                    ZipFile.ExtractToDirectory(zipfile, folder);
                    return folder;
                }
            );
        }

        private void ShowPercentage(int progress)
        {
            if (null == _progress)
                return;
            decimal status = decimal.Divide(progress, _taskCount) * 100;
            _progress.Report((int)status);
        }
    }
}
