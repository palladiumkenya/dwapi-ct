using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class SyncCommand<TS,TD> : ISyncCommand<TS, TD> where TS:TempExtract where TD:ClientExtract
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IEMRRepository _emrRepository;
        private readonly SqlConnection _connection;
        private SyncSummary _summary;
        private ExtractSetting _extractSetting;

        public SyncCommand(IEMRRepository emrRepository)
        {
            _emrRepository = emrRepository;
            _connection = _emrRepository.GetConnection() as SqlConnection;
        }

        public SyncSummary Summary => _summary;

        public virtual void Execute()
        {
            _summary=new SyncSummary();
          

            var extract = (TD)Activator.CreateInstance(typeof(TD));
            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = extract.GetAddAction(typeof(TS).Name);
                   _summary.Total= command.ExecuteNonQuery();
                }

            }
        }

        public virtual async  Task<SyncSummary> ExecuteAsync(IProgress<DProgress> progress = null)
        {
            _summary = new SyncSummary();
            var extractName = typeof(TS).Name;
            string statusUpdate = $"Importing";

            Log.Debug($"Executing Sync {extractName} command...");

            var emr = _emrRepository.GetDefault();
            if (null == emr) throw new Exception($"No Default EMR Setup !");

            _extractSetting = emr.GetActiveExtractSetting($"{extractName}");
            if (null == _extractSetting) throw new Exception($"No Extract Setting found for {emr}");

            progress?.ReportStatus($"{statusUpdate}...");

            EventHistory currentHistory = _emrRepository.GetStats(_extractSetting.Id);

            var extract = (TD)Activator.CreateInstance(typeof(TD));
            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                using (var command = _connection.CreateCommand())
                {
                    try
                    {
                        //var tx = _connection.BeginTransaction();
                        command.CommandText = extract.GetAddAction(typeof(TS).Name);
                        _summary.Total = await command.ExecuteNonQueryAsync();
                        //tx.Commit();

                        //update stats
                        _emrRepository.UpdateStats(_extractSetting.Id, StatAction.Imported, _summary.Total);
                        try
                        {
                            var notImported = currentHistory.Loaded.Value - _summary.Total;
                            if (notImported > -1)
                                _emrRepository.UpdateStats(_extractSetting.Id, StatAction.NotImported, notImported);
                        }
                        catch { }
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e);
                        throw;
                    }
                    
                    
                }

            }

            progress?.ReportStatus($"{statusUpdate} Finished");

            return _summary;
        }
    }
}