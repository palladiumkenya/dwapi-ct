using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public class SyncCommand<TS,TD> : ISyncCommand<TS, TD> where TS:TempExtract where TD:ClientExtract
    {
        private readonly IEMRRepository _emrRepository;
        private readonly IDbConnection _connection;
        private SyncSummary _summary;

        public SyncCommand(IEMRRepository emrRepository)
        {
            _emrRepository = emrRepository;
            _connection = _emrRepository.GetConnection();
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

        public virtual async  Task<SyncSummary> ExecuteAsync()
        {
            return await Task.Run(() =>
            {
                Execute();
                return Summary;
            });
        }
    }
}