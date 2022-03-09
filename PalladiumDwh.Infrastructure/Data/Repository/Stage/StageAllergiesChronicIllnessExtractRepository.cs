using System.Reflection;
using AutoMapper;
using log4net;
using PalladiumDwh.Core.Application.Stage.Repositories;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageAllergiesChronicIllnessExtractRepository :
        StageExtractRepository<StageAllergiesChronicIllnessExtract, AllergiesChronicIllnessExtract>, IStageAllergiesChronicIllnessExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DwapiCentralContext _context;
        private readonly IMapper _mapper;

        public StageAllergiesChronicIllnessExtractRepository(DwapiCentralContext context, IMapper mapper,
            string stageName = nameof(StageAllergiesChronicIllnessExtract), string extractName = nameof(AllergiesChronicIllnessExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}