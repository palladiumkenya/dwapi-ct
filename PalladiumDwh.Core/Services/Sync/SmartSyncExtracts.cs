using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Interfaces.Sync;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Services.Sync
{
    public class SmartSyncExtract:ISyncExtract
    {
        private readonly IPatientExtractRepository _patientExtractRepository;

        public SmartSyncExtract(IPatientExtractRepository patientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
        }

        public async Task ProcessPrimary(List<PatientExtractDto> patients)
        {
            var sitePatients = patients
                .GroupBy(x => x.SiteCode)
                .ToList();

            foreach (var site in sitePatients)
            {
                await _patientExtractRepository.BulkInsertOrUpdate(null);
            }
        }
    }
}
