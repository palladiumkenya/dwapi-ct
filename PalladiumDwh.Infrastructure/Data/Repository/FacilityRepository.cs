using System.Data.Entity;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        private readonly DwhServerContext _context;
        public FacilityRepository(DwhServerContext context) : base(context)
        {
            _context = context;
        }
    }
}