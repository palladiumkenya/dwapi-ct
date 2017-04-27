using System.Collections.Generic;
using System.Data;
using System.Linq;
using PagedList;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using Dapper;

namespace PalladiumDwh.ClientUploader.Infrastructure.Data
{
    public class ClientPatientRepository : IClientPatientRepository
    {
        private readonly DwapiRemoteContext _context;
        private IDbConnection db;

        public ClientPatientRepository(DwapiRemoteContext context)
        {
            _context = context;
            db = _context.Database.Connection;
        }

        public IEnumerable<ClientPatientExtract> GetAll()
        {
            return _context.ClientPatientExtracts.AsNoTracking();
        }

        public IPagedList<ClientPatientExtract> GetAll(int? page, int pageSize = 200)
        {
            
            var list = _context.ClientPatientExtracts.AsNoTracking()
                .OrderBy(x => x.PatientID);

            int totalUserCount = list.Count();

            if (pageSize == -1)
                return new PagedList<ClientPatientExtract>(list,1,totalUserCount);
                
                

            var pageNumber = page ?? 1;
            return new PagedList<ClientPatientExtract>(list, pageNumber, pageSize);

        }

        //TODO: select unprocessed extracts only
        public IEnumerable<ClientPatientExtract> GetAll(bool processed)
        {
           return GetAll().Where(x => x.Processed == processed||x.Processed==null);
        }

        public void UpdatePush(ClientPatientExtract patient,string profileExtract, PushResponse response)
        {
            //[QueueId] ,[Status] ,[StatusDate]
        string query = $"UPDATE PatientExtract SET Processed = 1 WHERE PatientPK = @PatientPK AND SiteCode=@SiteCode AND (Processed=0 or Processed Is Null);";
            if(!string.IsNullOrWhiteSpace(profileExtract))
                query += $"UPDATE {profileExtract} " +
                         $"SET Processed = 1,[QueueId]='{response.QueueId}',[Status]='{response.Status}',[StatusDate]='{response.StatusDate:yyyy-MMM-dd}' " +
                         $"WHERE PatientPK = @PatientPK AND SiteCode=@SiteCode AND (Processed=0 or Processed Is Null);";
            var count = this.db.Execute(query, patient);
        }
    }
}
