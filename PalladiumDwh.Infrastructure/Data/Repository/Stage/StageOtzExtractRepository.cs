using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageOtzExtractRepository :
        StageExtractRepository<StageOtzExtract, OtzExtract>, IStageOtzExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageOtzExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageOtzExtract), string extractName = nameof(OtzExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}