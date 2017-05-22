using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class ImportService : IImportService
    {
        private readonly IClientPatientExtractRepository _clientPatientExtractRepository;

        public ImportService(IClientPatientExtractRepository clientPatientExtractRepository)
        {
            _clientPatientExtractRepository = clientPatientExtractRepository;
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
    }
}
