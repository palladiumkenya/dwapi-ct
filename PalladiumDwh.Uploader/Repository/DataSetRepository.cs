using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class DataSetRepository : IDataSetRepository
    {
         public DwhClientEntities Context;


         public DataSetRepository(DwhClientEntities context)
        {
            Context = context;
        }

        public IEnumerable<DataSet> Get()
        {
            return Context.DataSets.ToList();
        }

        public DataSet Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(DataSet entity)
        {
            Context.DataSets.Add(entity);
            Context.SaveChanges();
        }

        public void Put(int id, DataSet entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
