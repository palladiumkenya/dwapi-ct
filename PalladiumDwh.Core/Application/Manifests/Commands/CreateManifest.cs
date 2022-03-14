using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using log4net;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Commands
{
    public class CreateManifest:IRequest<Result>
    {
        public Manifest Manifest { get; }
        public MasterFacility MasterFacility { get; }
        public bool AllowSnapshot { get; }


        public CreateManifest(Manifest manifest, MasterFacility masterFacility, bool allowSnapshot)
        {
            Manifest = manifest;
            MasterFacility = masterFacility;
            AllowSnapshot = allowSnapshot;
        }
    }

    public class CreateManifestHandler:IRequestHandler<CreateManifest,Result>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IStagePatientExtractRepository _stagePatientExtractRepository;
        private readonly ISmartActionRegisterRepository _smartActionRegisterRepository;

        public CreateManifestHandler(IPatientExtractRepository patientExtractRepository,  IStagePatientExtractRepository stagePatientExtractRepository, ISmartActionRegisterRepository smartActionRegisterRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _stagePatientExtractRepository = stagePatientExtractRepository;
            _smartActionRegisterRepository = smartActionRegisterRepository;
        }


        public async Task<Result> Handle(CreateManifest request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Debug("saving manifest");

                var facManifest = FacilityManifest.Create(request.Manifest);
                _patientExtractRepository.SaveManifest(facManifest);
                await _smartActionRegisterRepository.Clear(request.Manifest.SiteCode);
                await _stagePatientExtractRepository.ClearSite(request.MasterFacility.FacilityId.Value);

                return Result.Ok();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return  Result.Fail(e.Message);
            }
        }
    }
}
