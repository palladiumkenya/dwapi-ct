using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class ExportService:IExportService
    {
        
        private readonly IClientPatientExtractRepository _clientPatientExtractRepository;
        

        public ExportService(IClientPatientExtractRepository clientPatientExtractRepository)
        {
            _clientPatientExtractRepository = clientPatientExtractRepository;
        }

        public async Task<string> ExportToJSonAsync(string exportDir = "")
        {
            string folderToSaveTo;

            folderToSaveTo = string.IsNullOrWhiteSpace(exportDir) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : exportDir;


            folderToSaveTo = folderToSaveTo.EndsWith("\\") ? folderToSaveTo : $"{folderToSaveTo}\\";
            folderToSaveTo = $@"{folderToSaveTo}Exports\{DateTime.Today:yyyyMMMdd}\";

            bool exists = Directory.Exists(folderToSaveTo);

            if (!exists)
                Directory.CreateDirectory(folderToSaveTo);

            var patients = await Task.Run(() => _clientPatientExtractRepository.GetAll());
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
            }

            return folderToSaveTo;
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
    }
}
