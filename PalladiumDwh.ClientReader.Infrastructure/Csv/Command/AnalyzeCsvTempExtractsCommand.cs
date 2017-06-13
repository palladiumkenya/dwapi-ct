using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using log4net;
using PalladiumDwh.ClientReader.Core;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Model.Source.Map;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
    public class AnalyzeCsvTempExtractsCommand : IAnalyzeCsvTempExtractsCommand
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly DwapiRemoteContext _context;
        

        private IProgress<DProgress> _progress;
        private int _progressValue;
        private int _taskCount;
        

        public AnalyzeCsvTempExtractsCommand(DwapiRemoteContext context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<EventHistory>> ExecuteAsync(EMR emr, List<string> csvFiles,
            IProgress<DProgress> progress = null)
        {
            Log.Debug($"Executing AnalyzeCsvExtracts command...");
            List<EventHistory> eventHistories = new List<EventHistory>();

            if (null == emr)
            {
                Log.Debug($"no EMR set !");
                throw new ArgumentException("Default EMR has not been set !");
            }

            //Analyze Csv Extracts

            _taskCount = emr.ExtractSettings.Count;

            int count = 0;
            var extractSettings = emr.ExtractSettings.OrderByDescending(x => x.IsPriority);
            foreach (var extract in extractSettings)
            {
                count++;

                string stats = $"Analyzing CSV Extract {count} of {_taskCount} [{extract.Display}]";
                progress?.ReportStatus($"{stats}...");

                // Get All Csv files of Extract type

                var extractCsvName = $@"\{extract.ExtractCsv.HasToEndsWith(".csv")}";
                var matchingCsvFiles = csvFiles.Where(x => x.ToLower().Contains(extractCsvName.ToLower())).ToList();

                // Analyze each CSV file
                int matchingCsvFilesCount = matchingCsvFiles.Count;
                int csvCount = 0;

                foreach (var csv in matchingCsvFiles)
                {
                    csvCount++;
                    
                    var summaries = await Task.Run(() => GenerateFoundEventHistory(csv));
                    foreach (var s in summaries)
                    {
                        var eventHistory =
                            EventHistory.CreateFound(s.SiteCode, extract.Display, s.Found, extract.Id);

                        _context.EventHistories.Add(eventHistory);
                        await _context.SaveChangesAsync();
                    }
                    
                    progress?.ReportStatus($"{stats}",csvCount,matchingCsvFilesCount);
                }
            }


            return eventHistories;
        }

        private IEnumerable<DiscoverSource> ReadSourceCommand(string extractSql)
        {
            IEnumerable<DiscoverSource> discoverSources;
            using (TextReader txtReader = File.OpenText(extractSql))
            {
                var reader = new CsvReader(txtReader, GetConfig());
                reader.Configuration.RegisterClassMap(TempExtractMap.GetMap<DiscoverSource>());
                 discoverSources = reader.GetRecords<DiscoverSource>().ToList();
            }
            return discoverSources;
        }

        public IEnumerable<EventHistory> GenerateFoundEventHistory(string csvFile)
        {

            var discoverSources = ReadSourceCommand(csvFile);
            return discoverSources.GroupBy(a => a.SiteCode).Select(g => new EventHistory { SiteCode = g.Key, Found = g.Count() });
        }


        private CsvConfiguration GetConfig()
        {
            return new CsvConfiguration()
            {
                IsHeaderCaseSensitive = false,
                WillThrowOnMissingField = false,
                SkipEmptyRecords = true,
                IgnoreHeaderWhiteSpace = true,
                TrimFields = true,
                TrimHeaders = true
            };

        }
    }
}