using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data.MySqlClient;
using Npgsql;
using PalladiumDwh.ClientReader.Core.Enums;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using System.Data.Entity;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class EMRRepository: ClientRepository<EMR>, IEMRRepository
    {
        public EMRRepository(DwapiRemoteContext context) : base(context)
        {
        }

        public void SetEmrAsDefault(Guid id)
        {
            var currentEmr = Context.Emrs.Find(id);

            if (null != currentEmr)
            {
                var alldefualt = Context.Emrs.Where(x => x.IsDefault).ToList();
                foreach (var d in alldefualt)
                {
                    d.IsDefault = false;
                    Update(d);
                }
                currentEmr.IsDefault = true;
                Update(currentEmr);
            }
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(Context.Database.Connection.ConnectionString);
        }

        public IDbConnection GetEmrConnection()
        {
            DbConnection connection = null;
            var emr = GetDefault();
            if (null != emr)
            {
                var cn = ConfigurationManager.ConnectionStrings[$"{emr.ConnectionKey}"];
                if (null != cn)
                {
                    var connectionString = cn.ConnectionString;
                    var providerName = cn.ProviderName;

                    if (providerName.ToLower().Contains("System.Data.SqlClient".ToLower()))
                    {
                        connection = new SqlConnection(connectionString);
                    }
                    if (providerName.ToLower().Contains("MySql.Data.MySqlClient".ToLower()))
                    {
                        connection = new MySqlConnection(connectionString);
                    }
                    if (providerName.ToLower().Contains("Npgsql".ToLower()))
                    {
                        connection = new NpgsqlConnection(connectionString);
                    }
                }
            }
            return connection;
        }

     

        public EMR GetDefault()
        {
            return Context.Emrs.FirstOrDefault(x => x.IsDefault);
        }

        public void UpdateStats(EventHistory eventHistory, StatAction action, int count)
        {
            if (action == StatAction.Found)
            {
                eventHistory.Found = count;
                Context.EventHistories.Add(eventHistory);
            }
            if (action == StatAction.Loaded)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.Loaded = count;
                    eventHistoryToUpdate.LoadDate = DateTime.Now;
                    Context.EventHistories.Attach(eventHistoryToUpdate);
                    Context.Entry(eventHistoryToUpdate).State = EntityState.Modified;
                }
            }
            if (action == StatAction.Rejected)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.Rejected = count;
                    Context.EventHistories.Attach(eventHistoryToUpdate);
                    Context.Entry(eventHistoryToUpdate).State = EntityState.Modified;
                }
            }

            if (action == StatAction.Sent)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.Sent = count;
                    eventHistoryToUpdate.SendDate = DateTime.Now;
                    Context.EventHistories.Attach(eventHistoryToUpdate);
                    Context.Entry(eventHistoryToUpdate).State = EntityState.Modified;
                }
            }

            if (action == StatAction.NotSent)
            {
                var eventHistoryToUpdate = GetStats(eventHistory.ExtractSettingId);
                if (null != eventHistoryToUpdate)
                {
                    eventHistoryToUpdate.NotSent = count;
                    Context.EventHistories.Attach(eventHistoryToUpdate);
                    Context.Entry(eventHistoryToUpdate).State = EntityState.Modified;
                }
            }

        }

        public EventHistory GetStats(Guid extractSettingId)
        {
            return Context.EventHistories.FirstOrDefault(x => x.ExtractSettingId == extractSettingId);
        }
    }
}