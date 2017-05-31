using System;
using System.Collections.Generic;
using System.Dynamic;
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
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class ImportService : IImportService
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IClientPatientExtractRepository _clientPatientExtractRepository;
        private IProgress<int> _progress;
        private IProgress<DProgress> _dprogress;
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
            IProgress<DProgress> progress = null)
        {
            var importManifests = new List<ImportManifest>();

            string parentFolder = "";
            string folderToSaveTo = "";
            int fileCount =_taskCount= exportFiles.Count;

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
            int count = 0;
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

                count++;
                progress.ReportStatus("Extracting...", count, fileCount);
            }

            return importManifests;
        }

        public async Task<IEnumerable<SiteManifest>> ReadExportsAsync(string importDir="", IProgress<DProgress> progress = null)
        {
            string folderToImportFrom = string.Empty;

            if (string.IsNullOrWhiteSpace(importDir))
            {
                //save to My Documents
                folderToImportFrom = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderToImportFrom = $@"{folderToImportFrom.HasToEndsWith(@"\")}DWapi\Imports\".HasToEndsWith(@"\");
            }
            else
            {
                //TODO: check import folder
                folderToImportFrom = importDir;
            }

            folderToImportFrom = folderToImportFrom.HasToEndsWith(@"\");
            
            List<SiteManifest> siteManifests = new List<SiteManifest>();
           
            bool exists = Directory.Exists(folderToImportFrom);

            if (!exists)
                throw new ArgumentException($"Folder {folderToImportFrom} doesnt exist");

            var siteFolders= await Task.Run(() => Directory.GetDirectories(folderToImportFrom));

            int folderCount = siteFolders.Length;
            int count = 0;

            foreach (var siteFolder in siteFolders)
            {
                count++;
                //read manifest
                var manifestFiles = await Task.Run(() => Directory.GetFiles(siteFolder, "*.manifest"));

                var manifestFile = manifestFiles.First();
                if (manifestFile != null)
                {
                    var manifestFileConntent = await Task.Run(() => File.ReadAllText(manifestFile));
                    var siteManifest = SiteManifest.Create(manifestFileConntent, siteFolder);

                    if (siteManifest.ReadComplete)
                    {
                        var profileFiles = await Task.Run(() => Directory.GetFiles(siteFolder, "*.dwh"));

                        int profileFileCount = profileFiles.Length;
                        int pcount = 0;
                        foreach (var pf in profileFiles)
                        {
                            //Create profile
                            var profileContent = await Task.Run(() =>
                            {
                                var raw = File.ReadAllText(pf);
                                return Base64Decode(raw);
                            });

                            siteManifest.AddProfie(profileContent);
                            pcount++;
                            progress.ReportStatus($"Loading Export {count} of {folderCount} (Reading profile {pcount}/{profileFileCount})...", count, folderCount);
                        }
                    }
                    siteManifests.Add(siteManifest);

                }

                
                progress.ReportStatus($"Loading Export {count} of {folderCount}", count, folderCount);

            }

            return siteManifests;
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
        private void ShowDPercentage(DProgress progress)
        {
            if (null == _progress)
                return;
            _dprogress.Report(progress);
        }
    }
}
