using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageCervicalCancerScreeningExtractRepository :
        StageExtractRepository<StageCervicalCancerScreeningExtract, CervicalCancerScreeningExtract>, IStageCervicalCancerScreeningExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageCervicalCancerScreeningExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageCervicalCancerScreeningExtract), string extractName = nameof(CervicalCancerScreeningExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}