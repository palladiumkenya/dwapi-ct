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
using System.Threading.Tasks;
using Dapper;

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

        public int CreateStats(EventHistory eventHistory, StatAction action)
        {
            var db = Context.Database.Connection as SqlConnection;

            if (action == StatAction.Found)
            {
                try
                {
                    return db.Execute(@"
                    INSERT INTO EventHistory
                        (Id,SiteCode,Display,Found,FoundDate,ExtractSettingId)
                    VALUES
                        (@Id,@SiteCode,@Display,@Found,@FoundDate,@ExtractSettingId)", eventHistory);
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    throw;
                }
            }
            return -1;
        }

        public int UpdateStats(Guid extractSettingId, StatAction action, int count)
        {
            var db = Context.Database.Connection;
            var eventHistoryToUpdate = new EventHistory {ExtractSettingId = extractSettingId};

            if (action == StatAction.Loaded)
            {
                eventHistoryToUpdate.Loaded = count;
                eventHistoryToUpdate.LoadDate = DateTime.Now;

                try
                {
                    return db.Execute(@"
                                UPDATE EventHistory 
                                SET Loaded=@Loaded,LoadDate=@LoadDate
                                WHERE (ExtractSettingId=@ExtractSettingId)
                                ", eventHistoryToUpdate);
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    throw;
                }
                
            }
            if (action == StatAction.Rejected)
            {
                eventHistoryToUpdate.Rejected = count;
                try
                {
                    return db.Execute(@"
                                UPDATE EventHistory 
                                SET Rejected=@Rejected
                                WHERE (ExtractSettingId=@ExtractSettingId)
                                ", eventHistoryToUpdate);
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    throw;
                }
            }

            if (action == StatAction.Sent)
            {
                eventHistoryToUpdate.Sent = count;
                eventHistoryToUpdate.SendDate = DateTime.Now;
                try
                {
                    return db.Execute(@"
                                UPDATE EventHistory 
                                SET Sent=@Sent,SendDate=@SendDate
                                WHERE (ExtractSettingId=@ExtractSettingId)
                                ", eventHistoryToUpdate);
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    throw;
                }

            }

            if (action == StatAction.NotSent)
            {
                eventHistoryToUpdate.NotSent = count;
                try
                {
                    return db.Execute(@"
                                UPDATE EventHistory 
                                SET NotSent=@NotSent
                                WHERE (ExtractSettingId=@ExtractSettingId)
                                ", eventHistoryToUpdate);
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    throw;
                }
            }
            return -1;
        }

        public EventHistory GetStats(Guid extractSettingId)
        {
            var db = Context.Database.Connection;
            return db.QueryFirstOrDefault<EventHistory>(@"
                    SELECT        
	                    -1 AS SiteCode, 
	                    Display, 
	                    SUM(Found) AS Found, 
	                    MAX(FoundDate) AS FoundDate, 
	                    MAX(Loaded) AS Loaded, 
	                    MAX(Rejected) AS Rejected, 
	                    MAX(LoadDate) AS LoadDate, 
	                    MAX(Sent) AS Sent, 
	                    MAX(NotSent) AS NotSent, 
	                    MAX(SendDate) AS SendDate
                    FROM            
	                    EventHistory
                    WHERE        
	                    (ExtractSettingId = @ExtractSettingId)
                    GROUP BY 
	                    Display", new {ExtractSettingId = extractSettingId});
        }
    }
}