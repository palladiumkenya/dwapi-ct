using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ValidatePatientVisitExtractCommand : ValidateExtractCommand<TempPatientVisitExtract>, IValidatePatientVisitExtractCommand
  {
      public ValidatePatientVisitExtractCommand(IEMRRepository emrRepository, IValidatorRepository validatorRepository, int batchSize = 2000) : base(emrRepository, validatorRepository, batchSize)
      {
      }
  }
}
