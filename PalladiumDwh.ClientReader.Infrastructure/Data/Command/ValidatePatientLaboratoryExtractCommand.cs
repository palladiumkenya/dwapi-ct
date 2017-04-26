using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ValidatePatientLaboratoryExtractCommand : ValidateExtractCommand<TempPatientLaboratoryExtract>, IValidatePatientLaboratoryExtractCommand
  {
      public ValidatePatientLaboratoryExtractCommand(IEMRRepository emrRepository, IValidatorRepository validatorRepository, int batchSize = 2000) : base(emrRepository, validatorRepository, batchSize)
      {
      }
  }
}
