using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ValidatorRepository : ClientRepository<Validator>, IValidatorRepository
    {
        public ValidatorRepository(DwapiRemoteContext context) : base(context)
        {
        }


        public IEnumerable<Validator> GetByExtract(string extract)
        {
            return Context.Validators
                .Where(x => x.Extract.ToLower() == extract.ToLower());
        }
    }
}