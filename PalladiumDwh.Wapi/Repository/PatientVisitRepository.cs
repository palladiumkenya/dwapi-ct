using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Wapi.Models;

namespace PalladiumDwh.Wapi.Repository
{
    public class PatientVisitRepository : IPatientVisitRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        public DwhServerEntities Context;
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientVisitRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PatientVisitRepository(DwhServerEntities context)
        {
            Context = context;
        }
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientVisitExtract> Get()
        {
           return Context.PatientVisitExtracts.ToList();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PatientVisitExtract Get(int id)
        {
            return Context.PatientVisitExtracts.Find(id);
        }
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Post(PatientVisitExtract entity)
        {
            Context.PatientVisitExtracts.Add(entity);
            Context.SaveChanges();
        }
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public void Put(int id, PatientVisitExtract entity)
        {
            var patientVisitExtract = Context.PatientVisitExtracts.Find(id);
            if (patientVisitExtract == null) return;
            patientVisitExtract.Id = entity.Id;
            patientVisitExtract.PatientId = entity.Id;
            patientVisitExtract.PatientCccNumber = entity.PatientCccNumber;
            patientVisitExtract.SiteCode = entity.SiteCode;
            patientVisitExtract.VisitID = entity.VisitID;
            patientVisitExtract.VisitDate = entity.VisitDate;
            patientVisitExtract.Service = entity.Service;
            patientVisitExtract.VisitType = entity.VisitType;
            patientVisitExtract.WHOStage = entity.WHOStage;
            patientVisitExtract.WABStage = entity.WABStage;
            patientVisitExtract.Pregnant = entity.Pregnant;
            patientVisitExtract.LMP = entity.LMP;
            patientVisitExtract.EDD = entity.EDD;
            patientVisitExtract.Height = entity.Height;
            patientVisitExtract.Weight = entity.Weight;
            patientVisitExtract.BP = entity.BP;
            patientVisitExtract.OI = entity.OI;
            patientVisitExtract.OIDate = entity.OIDate;
            patientVisitExtract.SubstitutionFirstlineRegimenDate = entity.SubstitutionFirstlineRegimenDate;
            patientVisitExtract.SubstitutionFirstlineRegimenReason = entity.SubstitutionFirstlineRegimenReason;
            patientVisitExtract.SubstitutionSecondlineRegimenDate = entity.SubstitutionSecondlineRegimenDate;
            patientVisitExtract.SubstitutionSecondlineRegimenReason = entity.SubstitutionSecondlineRegimenReason;
            patientVisitExtract.SecondlineRegimenChangeDate = entity.SecondlineRegimenChangeDate;
            patientVisitExtract.SecondlineRegimenChangeReason = entity.SecondlineRegimenChangeReason;
            patientVisitExtract.Adherence = entity.Adherence;
            patientVisitExtract.AdherenceCategory = entity.AdherenceCategory;
            patientVisitExtract.FamilyPlanningMethod = entity.FamilyPlanningMethod;
            patientVisitExtract.PwP = entity.PwP;
            patientVisitExtract.GestationAge = entity.GestationAge;
            patientVisitExtract.NextAppointmentDate = entity.NextAppointmentDate;
            patientVisitExtract.Emr = entity.Emr;
            patientVisitExtract.Project = entity.Project;
            Context.SaveChanges();
        }
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            var patientVisitExtract = Context.PatientVisitExtracts.Find(id);
            if (patientVisitExtract == null) return;
            Context.PatientVisitExtracts.Find(id);
            Context.SaveChanges();
        }
    }
}