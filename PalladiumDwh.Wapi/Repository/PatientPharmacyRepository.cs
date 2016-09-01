using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    public class PatientPharmacyRepository : IPatientPharmacyRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        public DwhServerEntities Context;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientPharmacyRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PatientPharmacyRepository(DwhServerEntities context)
        {
            Context = context;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientPharmacyExtract> Get()
        {
            return Context.PatientPharmacyExtracts.ToList();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PatientPharmacyExtract Get(int id)
        {
            return Context.PatientPharmacyExtracts.Find(id);
        }
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Post(PatientPharmacyExtract entity)
        {
            Context.PatientPharmacyExtracts.Add(entity);
            Context.SaveChanges();
        }
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public void Put(int id, PatientPharmacyExtract entity)
        {
            var patientPharmacyExtract = Context.PatientPharmacyExtracts.Find(id);
            if (patientPharmacyExtract == null) return;
            patientPharmacyExtract.Id = entity.Id;
            patientPharmacyExtract.PatientId = entity.PatientId;
            patientPharmacyExtract.PatientCccNumber = entity.PatientCccNumber;
            patientPharmacyExtract.SiteCode = entity.SiteCode;
            patientPharmacyExtract.VisitID = entity.VisitID;
            patientPharmacyExtract.Drug = entity.Drug;
            patientPharmacyExtract.DispenseDate = entity.DispenseDate;
            patientPharmacyExtract.Duration = entity.Duration;
            patientPharmacyExtract.ExpectedReturn = entity.ExpectedReturn;
            patientPharmacyExtract.TreatmentType = entity.TreatmentType;
            patientPharmacyExtract.PeriodTaken = entity.PeriodTaken;
            patientPharmacyExtract.ProphylaxisType = entity.ProphylaxisType;
            patientPharmacyExtract.Emr = entity.Emr;
            patientPharmacyExtract.Project = entity.Project;
            Context.SaveChanges();
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var patientPharmacyExtract = Context.PatientPharmacyExtracts.Find(id);
            if (patientPharmacyExtract == null) return;
            Context.PatientPharmacyExtracts.Remove(patientPharmacyExtract);
            Context.SaveChanges();
        }
    }
}