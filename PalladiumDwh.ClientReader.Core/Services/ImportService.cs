﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

        public async Task<IEnumerable<ImportManifest>> ExtractExportsAsync(List<string> exportFiles, string importDir = "", IProgress<DProgress> progress = null)
        {
            var importManifests = new List<ImportManifest>();

            string parentFolder = "";
            string folderToSaveTo = "";
            int fileCount = _taskCount = exportFiles.Count;

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
                        importManifests.Add(ImportManifest.CreateSite(folder));
                    });
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                }

                count++;
                progress?.ReportStatus("Extracting...", count, fileCount);
            }

            return importManifests;
        }
        public async Task<IEnumerable<SiteManifest>> ReadExportsAsync(string importDir = "", IProgress<DProgress> progress = null)
        {
            string folderToImportFrom;

            if (string.IsNullOrWhiteSpace(importDir))
            {
                //Read from My Documents

                folderToImportFrom = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderToImportFrom = $@"{folderToImportFrom.HasToEndsWith(@"\")}DWapi\Imports\".HasToEndsWith(@"\");
                try
                {
                    if (!Directory.Exists(folderToImportFrom))
                        Directory.CreateDirectory(folderToImportFrom);
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    throw;
                }
            }
            else
            {
                //TODO: check import folder

                folderToImportFrom = importDir;
            }

            folderToImportFrom = folderToImportFrom.HasToEndsWith(@"\");

            var siteManifests = new List<SiteManifest>();

            bool exists = Directory.Exists(folderToImportFrom);

            if (!exists)
                throw new ArgumentException($"Folder {folderToImportFrom} NOT Found !");


            var siteFolders = await Task.Run(() => Directory.GetDirectories(folderToImportFrom));

            int folderCount = siteFolders.Length;
            int count = 0;

            foreach (var siteFolder in siteFolders)
            {

                count++;
                //read manifest
                await Task.Run(() =>
                {
                    var manifestFiles = Directory.GetFiles(siteFolder, "*.manifest");

                    var manifestFile = manifestFiles.FirstOrDefault();
                    if (manifestFile != null)
                    {
                        var manifestFileContent = File.ReadAllText(manifestFile);

                        var siteManifest = SiteManifest.Create(manifestFileContent, siteFolder);


                        siteManifests.Add(siteManifest);
                    }
                });


                progress?.ReportStatus($"Loading Export {count} of {folderCount}", count, folderCount);

            }

            return siteManifests;
        }

        public async Task<SiteManifest> GetSiteManifest(string importLocation , IProgress<DProgress> progress = null)
        {
            Log.Debug($"Reading export form [{importLocation}]...");

            SiteManifest siteManifest = null;
            importLocation = importLocation.HasToEndsWith(@"\");

            if (!Directory.Exists(importLocation))
            {
                Log.Debug($"Folder [{importLocation}] MISSING!");
                throw new ArgumentException($"Folder {importLocation} Not found");
            }

            var manifestFiles = Directory.GetFiles(importLocation, "*.manifest");
            var manifestFile = manifestFiles.First();
            
            if (manifestFile != null)
            {
                //Read Site Codes and PatientPKs
                var manifestFileContent = File.ReadAllText(manifestFile);

                siteManifest = SiteManifest.Create(manifestFileContent, importLocation);

                if (siteManifest.ReadComplete)
                {
                    Log.Debug($"Reading export form [{importLocation}] SiteInfo Found.");

                    var profileFiles = await Task.Run(() => Directory.GetFiles(importLocation, "*.dwh"));

                    int profileFileCount = profileFiles.Length;
                    siteManifest.ProfileCount = profileFileCount;

                    // Decode Patient Profiles

                    int pcount = 0;
                    foreach (var pf in profileFiles)
                    {

                        var profileContent = await Task.Run(() =>
                        {
                            var raw = File.ReadAllText(pf);
                            return Base64Decode(raw);
                        });

                        siteManifest.AddProfie(profileContent);
                        pcount++;
                        progress?.ReportStatus($"Reading profiles...", pcount,profileFileCount);
                    }
                }
                else
                {
                    Log.Debug($"Reading export form [{importLocation}] SiteInfo NOT FOUND !");
                }                
            }

           
            return siteManifest;
        }
        public async Task ClearExportsAsync(List<string> exportFolders, IProgress<DProgress> progress = null)
        {
            int folderCount = exportFolders.Count;
            int count = 0;

            foreach (var siteFolder in exportFolders)
            {
                count++;

                //remove folder

                bool exists = Directory.Exists(siteFolder);

                if (exists)
                {
                    

                    await
                        Task.Run(() =>
                        {
                            Directory.Delete(siteFolder, true);
                        });
                }

                progress?.ReportStatus($"Deleting Export {count} of {folderCount}", count, folderCount);
            }
        }

        public async Task CleanUpExportsAsync(string exportFolder, IProgress<DProgress> progress = null)
        {
            //remove folder

            progress?.ReportStatus($"Cleaning Exports..");


            bool exists = Directory.Exists(exportFolder);

                if (exists)
                {
                    // if manifest remains only

                    await
                        Task.Run(() =>
                        {
                            var files = Directory.GetFiles(exportFolder, "*.dwh");
                            if(files.Length==0)
                                Directory.Delete(exportFolder, true);
                        });
                }

                
         
        }

        public async  Task DeleteProfile(string exportProfile, IProgress<DProgress> progress = null)
        {
            bool exists = File.Exists(exportProfile);

            if (exists)
            {
                await
                    Task.Run(() =>
                    {
                        File.Delete(exportProfile);
                    });
            }
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

/*                    
                    if (siteManifest.ReadComplete)
                    {
                        var profileFiles = await Task.Run(() => Directory.GetFiles(siteFolder, "*.dwh"));

                        int profileFileCount = profileFiles.Length;
                        siteManifest.ProfileCount = profileFileCount;

                        // Create profile
                       
                        int pcount = 0;
                        foreach (var pf in profileFiles)
                        {
                            
                            var profileContent = await Task.Run(() =>
                            {
                                var raw = File.ReadAllText(pf);
                                return Base64Decode(raw);
                            });

                            siteManifest.AddProfie(profileContent);
                            pcount++;
                            progress?.ReportStatus($"Loading Export {count} of {folderCount} (Reading profile {pcount}/{profileFileCount})...", count, folderCount);
                        }
                       
                    }
                    */
