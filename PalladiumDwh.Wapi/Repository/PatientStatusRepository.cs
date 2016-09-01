using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    public class PatientStatusRepository : IPatientStatusRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        public DwhServerEntities Context;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientStatusRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PatientStatusRepository(DwhServerEntities context)
        {
            Context = context;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientStatusExtract> Get()
        {
           return Context.PatientStatusExtracts.ToList();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PatientStatusExtract Get(int id)
        {
            return Context.PatientStatusExtracts.Find(id);
        }
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Post(PatientStatusExtract entity)
        {
            Context.PatientStatusExtracts.Add(entity);
            Context.SaveChanges();
        }
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public void Put(int id, PatientStatusExtract entity)
        {
            var patientStatusExtract = Context.PatientStatusExtracts.Find(id);
            if (patientStatusExtract == null) return;
            patientStatusExtract.Id = entity.Id;
            patientStatusExtract.PatientId = entity.Id;
            patientStatusExtract.PatientCccNumber = entity.PatientCccNumber;
            patientStatusExtract.SiteCode = entity.SiteCode;
            patientStatusExtract.FacilityName = entity.FacilityName;
            patientStatusExtract.ExitDescription = entity.ExitDescription;
            patientStatusExtract.ExitDate = entity.ExitDate;
            patientStatusExtract.ExitReason = entity.ExitReason;
            patientStatusExtract.Emr = entity.Emr;
            patientStatusExtract.Project = entity.Project;
            Context.SaveChanges();
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var patientStatusExtract = Context.PatientStatusExtracts.Find(id);
            if (patientStatusExtract == null) return;
            Context.PatientStatusExtracts.Remove(patientStatusExtract);
            Context.SaveChanges();
        }
    }
}