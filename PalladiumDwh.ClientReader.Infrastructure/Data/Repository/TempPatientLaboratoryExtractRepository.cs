using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientLaboratoryExtractRepository : TempExtractRepository<TempPatientLaboratoryExtractError>,
        ITempPatientLaboratoryExtractRepository
    {
        public TempPatientLaboratoryExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}