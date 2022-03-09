using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Commands
{
    public class SyncManifest:IRequest
    {
        public Manifest Manifest { get; }
        public MasterFacility MasterFacility { get; }
        public bool AllowSnapshot { get; }


        public SyncManifest(Manifest manifest, MasterFacility masterFacility, bool allowSnapshot)
        {
            Manifest = manifest;
            MasterFacility = masterFacility;
            AllowSnapshot = allowSnapshot;
        }
    }

    public class SyncManifestHandler:IRequestHandler<SyncManifest>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFacilityRepository _facilityRepository;
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly ILiveSyncService _liveSyncService;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IActionRegisterRepository _actionRegisterRepository;

        public SyncManifestHandler( IFacilityRepository facilityRepository,IPatientExtractRepository patientExtractRepository, ILiveSyncService liveSyncService, JsonSerializerSettings serializerSettings, IActionRegisterRepository actionRegisterRepository)
        {
            _facilityRepository = facilityRepository;
            _patientExtractRepository = patientExtractRepository;
            _liveSyncService = liveSyncService;
            _actionRegisterRepository = actionRegisterRepository;
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public async Task<Unit> Handle(SyncManifest request, CancellationToken cancellationToken)
        {
            try
            {

                 Log.Debug("clearing Site Manifest...");
                await _patientExtractRepository.ClearManifest(request.Manifest);

                Log.Debug("removing Site Duplicates...");
                if (request.Manifest.UploadMode == UploadMode.Normal)
                {
                    Log.Debug("removing Site Duplicates...");
                    await _patientExtractRepository.RemoveDuplicates(request.Manifest.SiteCode);
                }

                Log.Debug("posting to SPOT...");
                var facManifest = FacilityManifest.Create(request.Manifest);
                var manifestDto = new ManifestDto(request.MasterFacility, facManifest);
                var metrics = MetricDto.Generate(request.MasterFacility, facManifest);
                var metricDtos = metrics.Where(x => x.CargoType != CargoType.Indicator).ToList();
                var indicatorDtos = metrics.Where(x => x.CargoType == CargoType.Indicator).ToList();
                manifestDto.Cargo =
                    JsonConvert.SerializeObject(ExtractDto.GenerateCargo(metricDtos), _serializerSettings);

                var result = await _liveSyncService.SyncManifest(manifestDto);

                if (metricDtos.Any())
                    await _liveSyncService.SyncMetrics(metricDtos);
                if (indicatorDtos.Any())
                {
                    var indstats = IndicatorDto.Generate(indicatorDtos);
                    await _liveSyncService.SyncIndicators(indstats);
                }


                var afacManifest = FacilityManifest.Create(request.Manifest);
                _patientExtractRepository.SaveManifest(afacManifest);
                await _actionRegisterRepository.Clear(request.Manifest.SiteCode);

                return Unit.Value;
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
