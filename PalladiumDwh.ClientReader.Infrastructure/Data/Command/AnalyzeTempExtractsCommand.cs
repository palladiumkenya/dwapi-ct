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
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class AnalyzeTempExtractsCommand : IAnalyzeTempExtractsCommand
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly DwapiRemoteContext _context;
        private readonly IDatabaseManager _databaseManager;
        private SqlConnection _connection;

        private IProgress<DProgress> _progress;
        private int _progressValue;
        private int _taskCount;
        

        public AnalyzeTempExtractsCommand(DwapiRemoteContext context, IDatabaseManager databaseManager)
        {
            _context = context;
            _databaseManager = databaseManager;
        }

        public async Task<IEnumerable<EventHistory>> ExecuteAsync(EMR emr, IProgress<DProgress> progress = null)
        {
            Log.Debug($"Executing AnalyzeExtracts command...");
            List<EventHistory> eventHistories = new List<EventHistory>();

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

                _taskCount = emr.ExtractSettings.Count;
                int count = 0;
                var extractSettings = emr.ExtractSettings.OrderByDescending(x => x.IsPriority);
                foreach (var extract in extractSettings)
                {
                    count++;
                    progress?.ReportStatus($"Analyzing Extracts [{extract.Display}]...",count,_taskCount);

                    var reader= await CountCommand(extract.ExtractSql);

                    using (reader)
                    {
                        if (null != reader)
                        {
                            while (reader.Read())
                            {
                                int? siteCode = (int?) reader.Get("SiteCode",typeof(int?));
                                int? found = (int?)reader.Get("NumOfTempRecors", typeof(int?));

                                var eventHistory =
                                    EventHistory.CreateFound(siteCode, extract.Display, found, extract.Id);

                                _context.EventHistories.Add(eventHistory);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            
            return eventHistories;
        }

        private  Task<SqlDataReader> CountCommand(string extractSql)
        {
            string sql = $@"
                SELECT        
	                 SiteCode,COUNT(PatientID) AS NumOfTempRecors
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