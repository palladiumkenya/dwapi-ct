using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.Stage.Repositories;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Bag;
using PalladiumDwh.Shared.Enum;

namespace PalladiumDwh.Core.Application.Commands
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

        public SyncPatientHandler(IMapper mapper, IStagePatientExtractRepository patientExtractRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _patientExtractRepository = patientExtractRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncPatient request, CancellationToken cancellationToken)
        {
            try
            {
                // standardize

                var extracts = _mapper.Map<List<StagePatientExtract>>(request.PatientSourceBag.Extracts);

                if (request.PatientSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
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
