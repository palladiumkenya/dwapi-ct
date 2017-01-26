using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profiles;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class SyncService : ISyncService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IReadPatientExtractCommand _readPatientExtractCommand;

        private readonly IFacilityRepository _facilityRepository;
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IPatientArtExtractRepository _patientArtExtractRepository;
        private readonly IPatientBaseLinesRepository _patientBaseLinesRepository;
        private readonly IPatientLabRepository _patientLabRepository;
        private readonly IPatientPharmacyRepository _patientPharmacyRepository;
        private readonly IPatientStatusRepository _patientStatusRepository;
        private readonly IPatientVisitRepository _patientVisitRepository;
        private readonly bool _portableMode = false;


        public SyncService(bool portableMode,

            IReadPatientExtractCommand readPatientExtractCommand,
            IFacilityRepository facilityRepository, IPatientExtractRepository patientExtractRepository,
            IPatientArtExtractRepository patientArtExtractRepository,
            IPatientBaseLinesRepository patientBaseLinesRepository, IPatientLabRepository patientLabRepository,
            IPatientPharmacyRepository patientPharmacyRepository, IPatientStatusRepository patientStatusRepository,
            IPatientVisitRepository patientVisitRepository)
        {
            _portableMode = portableMode;
            _readPatientExtractCommand = readPatientExtractCommand;

            _facilityRepository = facilityRepository;
            _patientExtractRepository = patientExtractRepository;
            _patientArtExtractRepository = patientArtExtractRepository;
            _patientBaseLinesRepository = patientBaseLinesRepository;
            _patientLabRepository = patientLabRepository;
            _patientPharmacyRepository = patientPharmacyRepository;
            _patientStatusRepository = patientStatusRepository;
            _patientVisitRepository = patientVisitRepository;
        }


        public IEnumerable<Facility> SyncFacilities(IList<Facility> facilities)
        {
            if (_portableMode)
            {
                var codes = facilities.Select(x => x.Code).ToList();
                _facilityRepository.ClearBy(codes);
            }
            else
            {
                _facilityRepository.Clear();
            }

            return _facilityRepository.Sync(facilities.ToList());
        }


        public void SyncPatients()
        {
            var facilities = new List<Facility>();

            var patients = _readPatientExtractCommand.Execute().ToList();

            if (patients.Count > 0)
            {

                var peAllfacilities =
                    patients.Select(x => new {x.SiteCode, x.FacilityName, x.Emr, x.Project}).Distinct().ToList();

                var peFacilities = peAllfacilities.Select(x => x.SiteCode).Distinct();
                foreach (var fcode in peFacilities)
                {

                    facilities.Add(new Facility(
                        fcode,
                        peAllfacilities.FirstOrDefault(x => x.SiteCode == fcode)?.FacilityName,
                        peAllfacilities.FirstOrDefault(x => x.SiteCode == fcode)?.Emr,
                        peAllfacilities.FirstOrDefault(x => x.SiteCode == fcode)?.Project

                    ));
                }

                var syncedFacilities= SyncFacilities(facilities);

                foreach (var p in patients)
                {
                    p.FacilityId=
                }
            }




        }
    }
}