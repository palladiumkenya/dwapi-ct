using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Services
{
   public class ReadExtractsService:IReadExtractService
   {
       private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

       private readonly IReadPatientExtractCommand _patientExtractCommand;
       private readonly IReadPatientArtExtractCommand _artExtractCommand;
       private readonly IReadPatientBaselinesExtractCommand _baselinesExtractCommand;
       private readonly IReadPatientLaboratoryExtractCommand _laboratoryExtractCommand;
       private readonly IReadPatientPharmacyExtractCommand _pharmacyExtractCommand;
       private readonly IReadPatientVisitExtractCommand _visitExtractCommand;
       private readonly IReadPatientStatusExtractCommand _statusExtractCommand;

        private IEnumerable<PatientExtractRow> _patientExtractRows;

        public ReadExtractsService(IReadPatientExtractCommand patientExtractCommand, IReadPatientArtExtractCommand artExtractCommand, IReadPatientBaselinesExtractCommand baselinesExtractCommand, IReadPatientLaboratoryExtractCommand laboratoryExtractCommand, IReadPatientPharmacyExtractCommand pharmacyExtractCommand, IReadPatientVisitExtractCommand visitExtractCommand, IReadPatientStatusExtractCommand statusExtractCommand)
       {
           _patientExtractCommand = patientExtractCommand;
           _artExtractCommand = artExtractCommand;
           _baselinesExtractCommand = baselinesExtractCommand;
           _laboratoryExtractCommand = laboratoryExtractCommand;
           _pharmacyExtractCommand = pharmacyExtractCommand;
           _visitExtractCommand = visitExtractCommand;
           _statusExtractCommand = statusExtractCommand;
       }

       

       public IEnumerable<Facility> GetFacilityData()
        {
            _patientExtractRows = _patientExtractCommand.Execute();

            var facilities = new List<Facility>();

            if (null == _patientExtractRows)
                return facilities;

           
            var allfacilities = _patientExtractRows
                .Select(x => new {x.SiteCode, x.FacilityName, x.Emr, x.Project})
                .Distinct()
                .ToList();

            foreach (var f in allfacilities)
            {
                facilities.Add(new Facility(
                    f.SiteCode,
                    f.FacilityName,
                    f.Emr,
                    f.Project
                ));
            }

            return facilities;
        }

        public IEnumerable<PatientExtract> GetPatientData(IList<Facility> facilities)
        {
            var patients = new List<PatientExtract>();

            if (null == facilities|| null == _patientExtractRows)
                return patients;

            if (facilities.Count==0)
                return patients;

            foreach (var e in _patientExtractRows)
            {
                var facility = facilities.FirstOrDefault(x => x.Code == e.SiteCode);

                if (null != facility)
                {
                    patients.Add(new PatientExtract()
                    {
                        PatientPID = e.PatientPK,
                        PatientCccNumber = e.PatientID,
                        Gender = e.Gender,
                        DOB = e.DOB,
                        RegistrationDate = e.RegistrationDate,
                        RegistrationAtCCC = e.RegistrationAtCCC,
                        RegistrationATPMTCT = e.RegistrationATPMTCT,
                        RegistrationAtTBClinic = e.RegistrationAtTBClinic,
                        PatientSource = e.PatientSource,
                        Region = e.Region,
                        District = e.District,
                        Village = e.Village,
                        ContactRelation = e.ContactRelation,
                        LastVisit = e.LastVisit,
                        MaritalStatus = e.MaritalStatus,
                        EducationLevel = e.EducationLevel,
                        DateConfirmedHIVPositive = e.DateConfirmedHIVPositive,
                        PreviousARTExposure = e.PreviousARTExposure,
                        PreviousARTStartDate = e.PreviousARTStartDate,
                        StatusAtCCC = e.StatusAtCCC,
                        StatusAtPMTCT = e.StatusAtPMTCT,
                        StatusAtTBClinic = e.StatusAtTBClinic,
                        FacilityId = facility.Id,
                        Emr = e.Emr,
                        Project = e.Project,
                    });
                }
                else
                {
                    Log.Debug($"Missing Facility ({e.SiteCode}-{e.FacilityName}) data for Patient {e.PatientID} !");
                }
            }

            return patients;
        }

        public IEnumerable<PatientArtExtract> GetPatientArtData(PatientExtract patient)
        {
            IList<PatientArtExtractRow> patientArtExtractRows;
            var patients = new List<PatientArtExtract>();

            if (null == patient || null == patientArtExtractRows)
                return patients;

            if (patientArtExtractRows.Count == 0)
                return patients;

            foreach (var e in patientArtExtractRows.Where(x => x.PatientPK == patient.PatientPID))
            {
                patients.Add(new PatientArtExtract()
                {
                    DOB = e.DOB,
                    AgeEnrollment = e.AgeEnrollment,
                    AgeARTStart = e.AgeARTStart,
                    AgeLastVisit = e.AgeLastVisit,
                    RegistrationDate = e.RegistrationDate,
                    Gender = e.Gender,
                    PatientSource = e.PatientSource,
                    StartARTDate = e.StartARTDate,
                    PreviousARTStartDate = e.PreviousARTStartDate,
                    PreviousARTRegimen = e.PreviousARTRegimen,
                    StartARTAtThisFacility = e.StartARTAtThisFacility,
                    StartRegimen = e.StartRegimen,
                    StartRegimenLine = e.StartRegimenLine,
                    LastARTDate = e.LastARTDate,
                    LastRegimen = e.LastRegimen,
                    LastRegimenLine = e.LastRegimenLine,
                    Duration = e.Duration,
                    ExpectedReturn = e.ExpectedReturn,
                    Provider = e.Provider,
                    LastVisit = e.LastVisit,
                    ExitReason = e.ExitReason,
                    ExitDate = e.ExitDate,
                    PatientId = patient.Id
                });
            }

            return patients;
        }

        public IEnumerable<PatientBaselinesExtract> GetPatientBaselineData(PatientExtract patient)
        {
            IList<PatientBaselinesExtractRow> patientExtractRows;
            var patients = new List<PatientBaselinesExtract>();

            if (null == patient || null == patientExtractRows)
                return patients;

            if (patientExtractRows.Count == 0)
                return patients;

            foreach (var e in patientExtractRows.Where(x => x.PatientPK == patient.PatientPID))
            {
                patients.Add(new PatientBaselinesExtract()
                {
                    bCD4 = e.bCD4,
                    bCD4Date = e.bCD4Date,
                    bWAB = e.bWAB,
                    bWABDate = e.bWABDate,
                    bWHO = e.bWHO,
                    bWHODate = e.bWHODate,
                    eWAB = e.eWAB,
                    eWABDate = e.eWABDate,
                    eCD4 = e.eCD4,
                    eCD4Date = e.eCD4Date,
                    eWHO = e.eWHO,
                    eWHODate = e.eWHODate,
                    lastWHO = e.lastWHO,
                    lastWHODate = e.lastWHODate,
                    lastCD4 = e.lastCD4,
                    lastCD4Date = e.lastCD4Date,
                    lastWAB = e.lastWAB,
                    lastWABDate = e.lastWABDate,
                    m12CD4 = e.m12CD4,
                    m12CD4Date = e.m12CD4Date,
                    m6CD4 = e.m6CD4,
                    m6CD4Date = e.m6CD4Date,
                    PatientId = patient.Id
                });
            }

            return patients;
        }

        public IEnumerable<PatientLaboratoryExtract> GetPatientLabData(PatientExtract patient)
        {
            IList<PatientLaboratoryExtractRow> patientExtractRows;
            var patients = new List<PatientLaboratoryExtract>();

            if (null == patient || null == patientExtractRows)
                return patients;

            if (patientExtractRows.Count == 0)
                return patients;

            foreach (var e in patientExtractRows.Where(x => x.PatientPK == patient.PatientPID))
            {
                patients.Add(new PatientLaboratoryExtract()
                {
                    VisitId = e.VisitId,
                    OrderedByDate = e.OrderedByDate,
                    ReportedByDate = e.ReportedByDate,
                    TestName = e.TestName,
                    EnrollmentTest = e.EnrollmentTest,
                    TestResult = e.TestResult,
                    PatientId = patient.Id
                });
            }

            return patients;
        }

        public IEnumerable<PatientPharmacyExtract> GetPatientPharmData(PatientExtract patient)
        {
            IList<PatientPharmacyExtractRow> patientExtractRows;
            var patients = new List<PatientPharmacyExtract>();

            if (null == patient || null == patientExtractRows)
                return patients;

            if (patientExtractRows.Count == 0)
                return patients;

            foreach (var e in patientExtractRows.Where(x => x.PatientPK == patient.PatientPID))
            {
                patients.Add(new PatientPharmacyExtract()
                {
                    VisitID = e.VisitID,
                    Drug = e.Drug,
                    Provider = e.Provider,
                    DispenseDate = e.DispenseDate,
                    Duration = e.Duration,
                    ExpectedReturn = e.ExpectedReturn,
                    TreatmentType = e.TreatmentType,
                    RegimenLine = e.RegimenLine,
                    PeriodTaken = e.PeriodTaken,
                    ProphylaxisType = e.ProphylaxisType,
                    PatientId = patient.Id
                });
            }

            return patients;
        }

        public IEnumerable<PatientStatusExtract> GetPatientStatusData(PatientExtract patient)
        {
            IList<PatientStatusExtractRow> patientExtractRows;
            var patients = new List<PatientStatusExtract>();

            if (null == patient || null == patientExtractRows)
                return patients;

            if (patientExtractRows.Count == 0)
                return patients;

            foreach (var e in patientExtractRows.Where(x => x.PatientPK == patient.PatientPID))
            {
                patients.Add(new PatientStatusExtract()
                {
                    ExitDescription = e.ExitDescription,
                    ExitDate = e.ExitDate,
                    ExitReason = e.ExitReason,
                    PatientId = patient.Id
                });
            }

            return patients;
        }

        public IEnumerable<PatientVisitExtract> GetPatientVisitData(PatientExtract patient)
        {
            IList<PatientVisitExtractRow> patientExtractRows;
            var patients = new List<PatientVisitExtract>();

            if (null == patient || null == patientExtractRows)
                return patients;

            if (patientExtractRows.Count == 0)
                return patients;

            foreach (var e in patientExtractRows.Where(x => x.PatientPK == patient.PatientPID))
            {
                patients.Add(new PatientVisitExtract()
                {
                    VisitId = e.VisitId,
                    VisitDate = e.VisitDate,
                    Service = e.Service,
                    VisitType = e.VisitType,
                    WHOStage = e.WHOStage,
                    WABStage = e.WABStage,
                    Pregnant = e.Pregnant,
                    LMP = e.LMP,
                    EDD = e.EDD,
                    Height = e.Height,
                    Weight = e.Weight,
                    BP = e.BP,
                    OI = e.OI,
                    OIDate = e.OIDate,
                    SubstitutionFirstlineRegimenDate = e.SubstitutionFirstlineRegimenDate,
                    SubstitutionFirstlineRegimenReason = e.SubstitutionFirstlineRegimenReason,
                    SubstitutionSecondlineRegimenDate = e.SubstitutionSecondlineRegimenDate,
                    SubstitutionSecondlineRegimenReason = e.SubstitutionSecondlineRegimenReason,
                    SecondlineRegimenChangeDate = e.SecondlineRegimenChangeDate,
                    SecondlineRegimenChangeReason = e.SecondlineRegimenChangeReason,
                    Adherence = e.Adherence,
                    AdherenceCategory = e.AdherenceCategory,
                    FamilyPlanningMethod = e.FamilyPlanningMethod,
                    PwP = e.PwP,
                    GestationAge = e.GestationAge,
                    NextAppointmentDate = e.NextAppointmentDate,
                    PatientId = patient.Id
                });
            }

            return patients;
        }
    }
}
