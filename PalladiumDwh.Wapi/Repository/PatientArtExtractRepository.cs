using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    public class PatientArtExtractRepository : IPatientArtExtractRepository
    {
        public DwhServerEntities Context;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientArtExtractRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PatientArtExtractRepository(DwhServerEntities context)
        {
            Context = context;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientArtExtract> Get()
        {
            return Context.PatientArtExtracts.ToList();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PatientArtExtract Get(int id)
        {
            return Context.PatientArtExtracts.Find(id);
        }
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Post(PatientArtExtract entity)
        {
            Context.PatientArtExtracts.Add(entity);
            Context.SaveChanges();
        }
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public void Put(int id, PatientArtExtract entity)
        {
            var patientArtExtract = Context.PatientArtExtracts.Find(id);
            if (patientArtExtract != null)
            {
                patientArtExtract.Id = entity.Id;
                patientArtExtract.PatientCccNumber = entity.PatientCccNumber;
                patientArtExtract.AgeEnrollment = entity.AgeEnrollment;
                patientArtExtract.AgeARTStart = entity.AgeARTStart;
                patientArtExtract.AgeLastVisit = entity.AgeLastVisit;
                patientArtExtract.SiteCode = entity.SiteCode;
                patientArtExtract.FacilityName = entity.FacilityName;
                patientArtExtract.RegistrationDate = entity.RegistrationDate;
                patientArtExtract.PatientSource = entity.PatientSource;
                patientArtExtract.Gender = entity.Gender;
                patientArtExtract.StartARTDate = entity.StartARTDate;
                patientArtExtract.PreviousARTStartDate = entity.PreviousARTStartDate;
                patientArtExtract.PreviousARTRegimen = entity.PreviousARTRegimen;
                patientArtExtract.StartARTAtThisFacility = entity.StartARTAtThisFacility;
                patientArtExtract.StartRegimen = entity.StartRegimen;
                patientArtExtract.StartRegimenLine = entity.StartRegimenLine;
                patientArtExtract.LastARTDate = entity.LastARTDate;
                patientArtExtract.LastRegimen = entity.LastRegimen;
                patientArtExtract.LastRegimenLine = entity.LastRegimenLine;
                patientArtExtract.Duration = entity.Duration;
                patientArtExtract.ExpectedReturn = entity.ExpectedReturn;
                patientArtExtract.LastVisit = entity.LastVisit;
                patientArtExtract.ExitReason = entity.ExitReason;
                patientArtExtract.ExitDate = entity.ExitDate;
                patientArtExtract.Emr = entity.Emr;
                patientArtExtract.Project = entity.Project;
                patientArtExtract.PatientId = entity.PatientId;
                Context.SaveChanges();

            }
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var patientArtExtract = Context.PatientArtExtracts.Find(id);
            if (patientArtExtract != null)
            {
                Context.PatientArtExtracts.Remove(patientArtExtract);
                Context.SaveChanges();
            }
        }
    }
}