using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class ImportCsvService : IImportCsvService
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int _taskCount;

        public async Task<List<string>> CopyCsvFilesAsync(List<string> exportFiles, string importDir = "",
            IProgress<DProgress> progress = null)
        {
            var importManifests = new List<string>();

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
            parentFolder = $@"{folderToSaveTo}DWapi\CsvImports\".HasToEndsWith(@"\");

            bool exists = Directory.Exists(parentFolder);

            if (!exists)
            {
                Directory.CreateDirectory(parentFolder);
            }

            //copy csvs extracts

            int count = 0;

            /*
            var folder =
                $"{parentFolder.HasToEndsWith(@"\")}{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}";
            folder = folder.HasToEndsWith(@"\");

            bool folderexists = Directory.Exists(folder);
            if (!folderexists)
            {
                Directory.CreateDirectory(folder);
            }
            */
            foreach (var f in exportFiles)
            {
                try
                {
                    var csvExtract= GetExtractByFileName(f);

                    await Task.Run(() =>
                    {
                        string csv = $"{parentFolder}{csvExtract}";
                        File.Copy(f, csv, true);
                        importManifests.Add(csv);
                    });

                }
                catch (Exception e)
                {
                    Log.Debug(e);
                }

                count++;
                progress?.ReportStatus("Copying...", count, fileCount);
            }

            return importManifests;
        }

        public async Task<IEnumerable<string>> ReadCsvFilesAsync(string csvImportDir = "", IProgress<DProgress> progress = null)
        {
            List<string> list=new List<string>();

            string folderToImportFrom;

            if (string.IsNullOrWhiteSpace(csvImportDir))
            {
                //Read from My Documents
                folderToImportFrom = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folderToImportFrom = $@"{folderToImportFrom.HasToEndsWith(@"\")}DWapi\CsvImports".HasToEndsWith(@"\");
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
                folderToImportFrom = csvImportDir;
            }

            folderToImportFrom = folderToImportFrom.HasToEndsWith(@"\");

            bool exists = Directory.Exists(folderToImportFrom);

            if (!exists)
                throw new ArgumentException($"Folder {folderToImportFrom} NOT Found !");


            var csvFiles = await Task.Run(() => Directory.GetFiles(folderToImportFrom, "*.csv"));
            list.AddRange(csvFiles);

            return list;
        }

        public string GetExtractByFileName(string filename)
        {
            string extract = string.Empty;

            var file = Path.GetFileNameWithoutExtension(filename);

            if (file.ToLower().Contains("ARTPatient".ToLower()))
            {
                extract = "ARTPatientExtract.csv";
            }
            else if (file.ToLower().Contains("PatientExtract".ToLower()) &&!file.ToLower().Contains("ARTPatient".ToLower()))
            {
                extract = "PatientExtract.csv";
            }
            else if (file.ToLower().Contains("Laboratory".ToLower()))
            {
                extract = "PatientLaboratoryExtract.csv";
            }
            else if (file.ToLower().Contains("Pharmacy".ToLower()))
            {
                extract = "PatientPharmacyExtract.csv";
            }
            else if (file.ToLower().Contains("Status".ToLower()))
            {
                extract = "PatientStatusExtract.csv";
            }
            else if (file.ToLower().Contains("Visit".ToLower()))
            {
                extract = "PatientVisitExtract.csv";
            }
            else if (file.ToLower().Contains("WHOCD4".ToLower()))
            {
                extract = "PatientWABWHOCD4Extract.csv";
            }

            return extract;
        }
    }
}