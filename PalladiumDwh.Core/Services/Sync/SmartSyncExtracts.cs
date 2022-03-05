using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Interfaces.Sync;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Bag;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Services.Sync
{
    public class SmartSyncExtract:ISyncExtract
    {
        private readonly IMapper _mapper;
        private readonly IStagePatientExtractRepository _patientExtractRepository;


        public SmartSyncExtract(IStagePatientExtractRepository patientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
        }

        public async Task ClearFacilitySession(PatientSourceBag patientSourceBag)
        {
            await _patientExtractRepository.ClearSite(patientSourceBag.FacilityId.Value, patientSourceBag.SessionId.Value);
        }

        public async Task ProcessPrimary(PatientSourceBag patientSourceBag)
        {
            // standardize

            var extracts = _mapper.Map<List<StagePatientExtract>>(patientSourceBag.Extracts);

            if (extracts.Any())
                extracts.ForEach(x =>x.Standardize(patientSourceBag));

            //stage

            await _patientExtractRepository.Stage(extracts, patientSourceBag.SessionId.Value);

        }
    }
}
