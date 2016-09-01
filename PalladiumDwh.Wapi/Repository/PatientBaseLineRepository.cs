using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    public class PatientBaseLineRepository: IPatientBaseLineRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        public DwhServerEntities Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientBaseLineRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PatientBaseLineRepository(DwhServerEntities context)
        {
            Context = context;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientBaselinesExtract> Get()
        {
            return Context.PatientBaselinesExtracts.ToList();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PatientBaselinesExtract Get(int id)
        {
            return Context.PatientBaselinesExtracts.Find(id);
        }
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Post(PatientBaselinesExtract entity)
        {
            Context.PatientBaselinesExtracts.Add(entity);
            Context.SaveChanges();
        }
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public void Put(int id, PatientBaselinesExtract entity)
        {
            var patientBaselineExtract = Context.PatientBaselinesExtracts.Find(id);
            if (patientBaselineExtract != null)
            {
                patientBaselineExtract.Id = entity.Id;
                patientBaselineExtract.PatientCccNumber = entity.PatientCccNumber;
                patientBaselineExtract.SiteCode = entity.SiteCode;
                patientBaselineExtract.eCD4 = entity.eCD4;
                patientBaselineExtract.eCD4Date = entity.eCD4Date;
                patientBaselineExtract.eWHO = entity.eWHO;
                patientBaselineExtract.eWHODate = entity.eWHODate;
                patientBaselineExtract.lastCD4 = entity.lastCD4;
                patientBaselineExtract.lastCD4Date = entity.lastCD4Date;
                patientBaselineExtract.m12CD4 = entity.m12CD4;
                patientBaselineExtract.m12CD4Date = entity.m12CD4Date;
                patientBaselineExtract.m6CD4 = entity.m6CD4;
                patientBaselineExtract.m6CD4Date = entity.m6CD4Date;
                patientBaselineExtract.Emr = entity.Emr;
                patientBaselineExtract.Project = entity.Project;
                patientBaselineExtract.PatientId = entity.PatientId;
                Context.SaveChanges();
            }
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var patientBaselineExtract = Context.PatientBaselinesExtracts.Find(id);
            if (patientBaselineExtract != null)
            {
                Context.PatientBaselinesExtracts.Remove(patientBaselineExtract);
                Context.SaveChanges();
            }
        }
    }
}