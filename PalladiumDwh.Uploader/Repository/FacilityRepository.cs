using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Repository
{
    public class FacilityRepository : IFacilityRepository
    {
          public DwhClientEntities Context;

          public FacilityRepository(DwhClientEntities context)
        {
            this.Context = context;
        }
        public IEnumerable<Facility> Get()
        {
            return Context.Facilities.ToList();
        }

        public Facility Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(Facility entity)
        {
            Context.Facilities.Add(entity);
            Context.SaveChanges();
        }

        public void Put(int id, Facility entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
