using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientUploader.Core.Interfaces;

namespace PalladiumDwh.ClientUploader.Infrastructure.Data
{
    public class ClientPatientRepository : IClientPatientRepository
    {
        private readonly DwapiRemoteContext _context;

        public ClientPatientRepository(DwapiRemoteContext context)
        {
            _context = context;
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
    }
}
