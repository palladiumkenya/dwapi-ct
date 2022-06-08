using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.Extracts.Notififactions;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Extentions;

namespace PalladiumDwh.Core.Application.Extracts.Commands
{
    public class SyncPatient : IRequest
    {
        public PatientSourceBag PatientSourceBag { get; }
        public SyncPatient(PatientSourceBag patientSourceBag)
        {
            PatientSourceBag = patientSourceBag;
        }
    }

    public class SyncPatientHandler : IRequestHandler<SyncPatient>
    {
        private readonly IMapper _mapper;
        private readonly IStagePatientExtractRepository _patientExtractRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;
        
        public SyncPatientHandler(IMapper mapper, IStagePatientExtractRepository patientExtractRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _patientExtractRepository = patientExtractRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncPatient request, CancellationToken cancellationToken)
        {
            try
            {

                // await _patientExtractRepository.ClearSite(request.PatientSourceBag.FacilityId.Value, request.PatientSourceBag.ManifestId.Value);

                // standardize

                var extracts = _mapper.Map<List<StagePatientExtract>>(request.PatientSourceBag.Extracts);

                if (request.PatientSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.PatientSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.PatientSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.PatientSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.PatientSourceBag, facs));

                }

                //stage
                await _patientExtractRepository.SyncStage(extracts, request.PatientSourceBag.ManifestId.Value);

                List<Guid> facIds = extracts.Select(x => x.FacilityId).Distinct().ToList();
                await _mediator.Publish(new SyncMainExtractEvent(facIds), cancellationToken);
                return Unit.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
