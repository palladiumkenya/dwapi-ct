
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IValidatorRepository: IClientRepository<Validator>
    {
        IEnumerable<Validator> GetByExtract(string extract);
    }
}