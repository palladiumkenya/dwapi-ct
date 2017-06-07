
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IValidatorRepository: IClientRepository<Validator>
    {
        IEnumerable<Validator> GetByExtract(string extract);
    }
}