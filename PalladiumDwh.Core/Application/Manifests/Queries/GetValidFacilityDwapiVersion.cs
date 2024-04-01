using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using MediatR;
using Newtonsoft.Json.Linq;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Manifests.Queries
{
    public class GetValidFacilityDwapiVersion:IRequest<MasterFacility>
    {
        public Manifest Manifest { get; }
        public bool AllowSnapshot { get; }
        public int CuttoffVer { get; }
        public int CurrentVer { get; }


        
        public GetValidFacilityDwapiVersion(Manifest manifest, int cuttoffVer, int currentVer)
        {
            Manifest = manifest;
            CuttoffVer = cuttoffVer;
            CurrentVer = currentVer;

        }
    }

    public class GetValidFacilityDwapiVersionHandler:IRequestHandler<GetValidFacilityDwapiVersion,MasterFacility>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IFacilityRepository _facilityRepository;


        public GetValidFacilityDwapiVersionHandler(IPatientExtractRepository patientExtractRepository, IFacilityRepository facilityRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<MasterFacility>  Handle(GetValidFacilityDwapiVersion request, CancellationToken cancellationToken)
        {
            
            var manifest = request.Manifest;

            var DwapiVersionCuttoff = request.CuttoffVer;
            
            var currentLatestVersion = request.CurrentVer;

            var DwapiVersionSending = JObject.Parse(manifest.FacMetrics.Select(o => o.Metric)
                .Where(x => x.Contains("CareTreatment")).ToList()[0]);
            if (Int32.Parse(DwapiVersionSending["Version"].ToString().Replace(".", string.Empty)) < DwapiVersionCuttoff)
            {
                throw new Exception($" ====> You're using DWAPI Version [{DwapiVersionSending["Version"]}]. Older Versions of DWAPI are not allowed to send to NDWH. UPGRADE to the latest version {currentLatestVersion} and RELOAD and SEND");
                
            }
           try
                {
                manifest.Validate();
                var masterFacility = await _patientExtractRepository.VerifyFacility(manifest.SiteCode);
                if (null == masterFacility)
                    throw new Exception($"SiteCode [{manifest.SiteCode}] NOT FOUND in Master Facility List");

                _facilityRepository.Enroll(masterFacility, manifest.EmrName,request.AllowSnapshot);

                return masterFacility;
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
