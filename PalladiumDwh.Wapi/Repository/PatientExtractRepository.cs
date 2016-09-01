using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    public class PatientExtractRepository : IPatientExtractRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        public DwhServerEntities Context;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientExtractRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PatientExtractRepository(DwhServerEntities context)
        {
            this.Context = context;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientExtract> Get()
        {
            return Context.PatientExtracts.ToList();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PatientExtract Get(int id)
        {
            return Context.PatientExtracts.Find(id);
        }
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Post(PatientExtract entity)
        {
            Context.PatientExtracts.Add(entity);
            Context.SaveChanges();
        }
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public void Put(int id, PatientExtract entity)
        {
            var patientExtract = Context.PatientExtracts.Find(id);
            if (patientExtract != null)
            {
                patientExtract.PatientCccNumber = entity.PatientCccNumber;
                patientExtract.SiteCode = entity.SiteCode;
                patientExtract.FacilityName = entity.FacilityName;
                patientExtract.Gender = entity.Gender;
                patientExtract.DOB = entity.DOB;
                patientExtract.RegistrationDate = entity.RegistrationDate;
                patientExtract.RegistrationAtCCC = entity.RegistrationAtCCC;
                patientExtract.RegistrationATPMTCT = entity.RegistrationATPMTCT;
                patientExtract.RegistrationAtTBClinic = entity.RegistrationAtTBClinic;
                patientExtract.PatientSource = entity.PatientSource;
                patientExtract.Region = entity.Region;
                patientExtract.District = entity.District;
                patientExtract.Village = entity.Village;
                patientExtract.ContactRelation = entity.ContactRelation;
                patientExtract.LastVisit = entity.LastVisit;
                patientExtract.MaritalStatus = entity.MaritalStatus;
                patientExtract.EducationLevel = entity.EducationLevel;
                patientExtract.DateConfirmedHIVPositive = entity.DateConfirmedHIVPositive;
                patientExtract.PreviousARTExposure = entity.PreviousARTExposure;
                patientExtract.PreviousARTStartDate = entity.PreviousARTStartDate;
                patientExtract.Emr = entity.Emr;
                patientExtract.Project = entity.Project;
                Context.SaveChanges();
            }
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var patientExtract = Context.PatientExtracts.Find(id);
            if (patientExtract != null)
            {
                Context.PatientExtracts.Remove(patientExtract);
                Context.SaveChanges();
            }
        }
    }
}