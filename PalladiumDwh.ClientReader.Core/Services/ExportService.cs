using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class ExportService:IExportService
    {
        
        private readonly IClientPatientExtractRepository _clientPatientExtractRepository;
        private IProgress<int> _progress;
        private int _progressValue;
        private int _taskCount;

        public ExportService(IClientPatientExtractRepository clientPatientExtractRepository)
        {
            _clientPatientExtractRepository = clientPatientExtractRepository;
        }

        public async Task<string> ExportToJSonAsync(string exportDir = "", IProgress<int> progress = null)
        {
            List<Manifest> currentManifests = new List<Manifest>();
            

            _progress = progress;

            var parentFolder = "";
            var folderToSaveTo = "";
            var siteCode = 0;
            

            var patients = await Task.Run(() => _clientPatientExtractRepository.GetAll().ToList());
            _taskCount = patients.Count;

            if (_taskCount > 0)
            {
                currentManifests = CreateManifests(patients);
                var currentSite = currentManifests.FirstOrDefault();
                if (null != currentSite)
                    siteCode = currentSite.SiteCode;
            }

            if (string.IsNullOrWhiteSpace(exportDir))
            {
                //save to My Documents
                folderToSaveTo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                folderToSaveTo = exportDir;
            }

            folderToSaveTo = folderToSaveTo.HasToEndsWith(@"\");
            parentFolder = $@"{folderToSaveTo}DWapi\Exports\".HasToEndsWith(@"\");

            folderToSaveTo = $@"{parentFolder}{DateTime.Today:yyyyMMMdd}-{siteCode}\".HasToEndsWith(@"\");
            

            bool exists = Directory.Exists(folderToSaveTo);

            if (!exists)
            {
                Directory.CreateDirectory(folderToSaveTo);
            }
            else
            {
                // Delete All
                Directory.Delete(folderToSaveTo,true);
                Directory.CreateDirectory(folderToSaveTo);
            }

            addManifest(folderToSaveTo, currentManifests);

            foreach (var p in patients)
            {
                var jsonPatient = JsonConvert.SerializeObject(p, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                //Encode
                jsonPatient = Base64Encode(jsonPatient);

                await Task.Run(() => System.IO.File.WriteAllText($"{folderToSaveTo}{p.Id}.dwh", jsonPatient));

                _progressValue++;
                ShowPercentage(_progressValue);
            }

            await ZipExtracts(folderToSaveTo);

            return parentFolder;
        }

        public  string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public  string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private Task ZipExtracts(string folder)
        {
            string zipped = folder.ReplaceFromEnd(@"\", ".zip");

            if (File.Exists(zipped))
                File.Delete(zipped);

            return Task.Run(() =>
                {
                     ZipFile.CreateFromDirectory(folder, zipped, CompressionLevel.Fastest, false);
                    Directory.Delete(folder, true);
                }
            );

        }

        private void addManifest(string folder, List<Manifest> currentManifests)
        {
            string fileName = $"{folder.HasToEndsWith(@"\")}dwapi.manifest";
            using (StreamWriter writer =
                new StreamWriter(fileName))
            {
                writer.Write(JsonConvert.SerializeObject(currentManifests));
            }
        }

        private void ShowPercentage(int progress)
        {
            if (null == _progress)
                return;
            decimal status = decimal.Divide(progress, _taskCount) * 100;
            _progress.Report((int)status);
        }

        private List<Manifest> CreateManifests(List<ClientPatientExtract> patients)
        {
            var manifests = new List<Manifest>();

            var siteCodes = patients
                .Select(x => x.SiteCode)
                .Distinct();

            foreach (var s in siteCodes)
            {
                var manifest = new Manifest(s);

                var pks = patients
                    .Where(x => x.SiteCode == s)
                    .Select(x => x.PatientPK)
                    .Distinct()
                    .ToList();

                if (null != pks)
                {
                    if (pks.Count > 0)
                        manifest.PatientPKs.AddRange(pks);
                }

                manifests.Add(manifest);
            }

            return manifests;
        }
    }
}
