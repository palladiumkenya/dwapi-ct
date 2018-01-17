using System;
using System.Activities.Expressions;
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
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using log4net;
using Z.Dapper.Plus;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class EMRRepository: ClientRepository<EMR>, IEMRRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public EMRRepository(DwapiRemoteContext context) : base(context)
        {
            //TODO : Dapper+
            DapperPlusManager.Entity<ClientPatientExtract>().Table("PatientExtract").Identity(x => x.Id);
            DapperPlusManager.Entity<ClientPatientArtExtract>().Table("PatientArtExtract").Identity(x => x.Id);
            DapperPlusManager.Entity<ClientPatientBaselinesExtract>().Table("PatientBaselinesExtract").Identity(x => x.Id);
            DapperPlusManager.Entity<ClientPatientLaboratoryExtract>().Table("PatientLaboratoryExtract").Identity(x => x.Id);
            DapperPlusManager.Entity<ClientPatientPharmacyExtract>().Table("PatientPharmacyExtract").Identity(x => x.Id);
            DapperPlusManager.Entity<ClientPatientStatusExtract>().Table("PatientStatusExtract").Identity(x => x.Id);
            DapperPlusManager.Entity<ClientPatientVisitExtract>().Table("PatientVisitExtract").Identity(x => x.Id);
         
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

            if (action == StatAction.Imported)
            {
                eventHistoryToUpdate.Imported = count;
                eventHistoryToUpdate.ImportDate = DateTime.Now;

                try
                {
                    return db.Execute(@"
                                UPDATE EventHistory 
                                SET Imported=@Imported,ImportDate=@ImportDate
                                WHERE (ExtractSettingId=@ExtractSettingId)
                                ", eventHistoryToUpdate);
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    throw;
                }

            }
            if (action == StatAction.NotImported)
            {
                eventHistoryToUpdate.NotImported = count;
                try
                {
                    return db.Execute(@"
                                UPDATE EventHistory 
                                SET NotImported=@NotImported
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

        public void ResetSendStats()
        {
            var db = GetConnection();

            //reset patients
            try
            {
                db.Execute(@"UPDATE PatientExtract SET Status='Sent' WHERE Processed=1 AND NOT(coalesce([Status],'')='Sent')");
            }
            catch (Exception e)
            {
                Log.Debug(e);
                throw;
            }
            

            using (db)
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();
                }

                //update NOT Sent patients
                string sqlNotSentPatients = @"
                        SELECT distinct PatientPK,SiteCode FROM [PatientArtExtract] Where NOT(coalesce([Status],'')='Sent') union
                        SELECT distinct PatientPK,SiteCode FROM [PatientBaselinesExtract] Where NOT(coalesce([Status],'')='Sent')  union
                        SELECT distinct PatientPK,SiteCode FROM [PatientLaboratoryExtract] Where NOT(coalesce([Status],'')='Sent') union
                        SELECT distinct PatientPK,SiteCode FROM [PatientPharmacyExtract] Where NOT(coalesce([Status],'')='Sent')  union
                        SELECT distinct PatientPK,SiteCode FROM [PatientStatusExtract] Where NOT(coalesce([Status],'')='Sent') union
                        SELECT distinct PatientPK,SiteCode FROM [PatientVisitExtract] Where NOT(coalesce([Status],'')='Sent')
                            ";

                using (var reader = db.ExecuteReader(sqlNotSentPatients))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            db.Execute(
                                $@"
                                    UPDATE 
                                        PatientExtract 
                                    SET Status='Not Sent' 
                                    WHERE 
                                        Processed=1 AND 
                                        PatientPK={reader[0]} AND 
                                        SiteCode={reader[1]}");
                        }
                        catch (Exception e)
                        {
                            Log.Debug(e);
                            throw;
                        }

                    }
                }

            }
        }

        public void UpdateSendStats(Guid extractSettingId)
        {
            var db = GetConnection();

            string tablename;

            try
            {
                tablename= db.QueryFirstOrDefault<string>(@"SELECT Destination FROM [ExtractSetting] WHERE Id=@Id",
                    new { Id = extractSettingId });
            }
            catch (Exception e)
            {
                Log.Debug(e);
                throw;
            }
             

            if (string.IsNullOrWhiteSpace(tablename))
                return;

            tablename = tablename.ToLower().Replace("Temp".ToLower(), "").ToLower().Trim();

            try
            {
                db.Execute($@"
                        
                        UPDATE       
	                        EventHistory
                        SET  
                            SendDate=(SELECT MAX(StatusDate) StatusDate FROM {tablename} WHERE (Status = 'Sent')),    
	                        Sent =(SELECT COUNT(PatientPK) AS Sent FROM {tablename} WHERE (Status='Sent')), 
	                        NotSent =(SELECT COUNT(PatientPK) AS NotSent FROM {tablename} WHERE NOT(coalesce([Status],'')='Sent'))                        
                        WHERE        
	                        (ExtractSettingId ='{extractSettingId}')");
            }
            catch (Exception e)
            {
                Log.Debug(e);
                throw;
            }
        }

        public EventHistory GetStats(Guid extractSettingId)
        {
            UpdateSendStats(extractSettingId);

            var db = GetConnection();

            try
            {
                return db.QueryFirstOrDefault<EventHistory>(@"
                    SELECT        
	                    -1 AS SiteCode, 
	                    Display, 
	                    SUM(Found) AS Found, 
	                    MAX(FoundDate) AS FoundDate, 
	                    MAX(Loaded) AS Loaded, 
	                    MAX(Rejected) AS Rejected, 
	                    MAX(LoadDate) AS LoadDate, 
                        MAX(Imported) AS Imported, 
	                    MAX(NotImported) AS NotImported, 
	                    MAX(ImportDate) AS ImportDate, 
	                    MAX(Sent) AS Sent, 
	                    MAX(NotSent) AS NotSent, 
	                    MAX(SendDate) AS SendDate
                    FROM            
	                    EventHistory
                    WHERE        
	                    (ExtractSettingId = @ExtractSettingId)
                    GROUP BY 
	                    Display", new { ExtractSettingId = extractSettingId });

            }
            catch (Exception e)
            {
                Log.Debug(e);
                throw;
            }
        }
    }
}