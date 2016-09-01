using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    public class PatientLabRepository : IPatientLabRepository
    {

        /// <summary>
        /// The context
        /// </summary>
        public DwhServerEntities Context;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientLabRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PatientLabRepository(DwhServerEntities context)
        {
            Context = context;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientLaboratoryExtract> Get()
        {
            return Context.PatientLaboratoryExtracts.ToList();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PatientLaboratoryExtract Get(int id)
        {
            return Context.PatientLaboratoryExtracts.Find(id);
        }
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Post(PatientLaboratoryExtract entity)
        {
            Context.PatientLaboratoryExtracts.Add(entity);
            Context.SaveChanges();
        }
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public void Put(int id, PatientLaboratoryExtract entity)
        {
            var patientLaboratoryExtract = Context.PatientLaboratoryExtracts.Find(id);
            if (patientLaboratoryExtract == null) return;
            patientLaboratoryExtract.Id = entity.Id;
            patientLaboratoryExtract.PatientId = entity.PatientId;
            patientLaboratoryExtract.PatientCccNumber = entity.PatientCccNumber;
            patientLaboratoryExtract.SiteCode = entity.SiteCode;
            patientLaboratoryExtract.VisitId = entity.VisitId;
            patientLaboratoryExtract.OrderedByDate = entity.OrderedByDate;
            patientLaboratoryExtract.ReportedByDate = entity.ReportedByDate;
            patientLaboratoryExtract.TestName = entity.TestName;
            patientLaboratoryExtract.TestResult = entity.TestResult;
            patientLaboratoryExtract.Emr = entity.Emr;
            patientLaboratoryExtract.Project = entity.Project;
            Context.SaveChanges();
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var patientLaboratoryExtract = Context.PatientLaboratoryExtracts.Find(id);
            if (patientLaboratoryExtract == null) return;
            Context.PatientLaboratoryExtracts.Remove(patientLaboratoryExtract);
            Context.SaveChanges();
        }
    }

}