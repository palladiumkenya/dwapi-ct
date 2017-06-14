using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class AnalyzeTempExtractsCommand : IAnalyzeTempExtractsCommand
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IEMRRepository _emrRepository;
        private readonly IDatabaseManager _databaseManager;
        private SqlConnection _connection;


        private IProgress<DProgress> _progress;
        private int _progressValue;
        private int _taskCount;

        public AnalyzeTempExtractsCommand(IEMRRepository emrRepository, IDatabaseManager databaseManager)
        {
            _emrRepository = emrRepository;
            _databaseManager = databaseManager;
        }

        public async Task<IEnumerable<EventHistory>> ExecuteAsync(IProgress<DProgress> progress = null)
        {
            Log.Debug($"Executing AnalyzeExtracts command...");
            List<EventHistory> eventHistories = new List<EventHistory>();


            var emr = _emrRepository.GetDefault();
            if (null == emr)
            {
                Log.Debug($"no EMR set !");
                throw new ArgumentException("Default EMR has not been set !");
            }

            var dbConfig = Read(emr.ConnectionKey);
            _connection = _databaseManager.GetConnection(dbConfig) as SqlConnection;
            if (null == _connection)
            {
                throw new ArgumentException("EMR database is not supported !");
            }

            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                //Analyze TempExtracts

                _taskCount = emr.ExtractSettings.Count+1;
                int count = 0;
                var extractSettings = emr.ExtractSettings.OrderByDescending(x => x.IsPriority);
                foreach (var extract in extractSettings)
                {
                    count++;
                    string stats = $"Analyzing Extract {count} of {_taskCount-1} [{extract.Display}]";
                    progress?.ReportStatus($"{stats}", count, _taskCount);
                    var reader= await CountCommand(extract.ExtractSql);

                    using (reader)
                    {
                        if (null != reader)
                        {
                            while (reader.Read())
                            {
                                int? siteCode = (int?) reader.Get("SiteCode",typeof(int?));
                                int? found = (int?)reader.Get("NumOfTempRecords", typeof(int?));

                                var eventHistory =
                                    EventHistory.CreateFound(siteCode, extract.Display, found, extract.Id);

                                _emrRepository.CreateStats(eventHistory, StatAction.Found);
                                
                            }
                        }
                    }
                    
                }

                progress?.ReportStatus($"Analyzing Extract Finished", _taskCount, _taskCount);
            }
            
            return eventHistories;
        }

        private  Task<SqlDataReader> CountCommand(string extractSql)
        {
            string sql = $@"
                SELECT        
	                 SiteCode,COUNT(PatientPK) AS NumOfTempRecords
                FROM            
	                (
                    {extractSql}
	                ) AS tmpExtract
                GROUP BY 
                    SiteCode
                ";

            return GetCommand(sql).ExecuteReaderAsync();
        }

        private SqlCommand GetCommand(string action)
        {
            var command = _connection.CreateCommand();
            command.CommandTimeout = 0;
            command.CommandText = $@" {action}; ";
            return command;
        }

        private DatabaseConfig Read(string key)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            var connectionString = connectionStringsSection.ConnectionStrings[$"{key}"].ConnectionString;
            var provider = connectionStringsSection.ConnectionStrings[$"{key}"].ProviderName;

            var dbtype = DatabaseType.GetAll().FirstOrDefault(x => x.Provider.ToLower() == provider.ToLower());

            var databaseConfig = _databaseManager.GetDatabaseConfig(dbtype.Provider, connectionString);
            databaseConfig.DatabaseType = dbtype;
            return databaseConfig;
        }


    }
}