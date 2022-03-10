using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageBaselineExtractRepository :
        StageExtractRepository<StageBaselineExtract, PatientBaselinesExtract>, IStageBaselineExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageBaselineExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageBaselineExtract), string extractName = nameof(PatientBaselinesExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
