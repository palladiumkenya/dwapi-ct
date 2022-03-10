using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using log4net;
using MediatR;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Manifests.Commands
{
    public class ClearDuplicates:IRequest<Result>
    {
        public Manifest Manifest { get; }

        public ClearDuplicates(Manifest manifest)
        {
            Manifest = manifest;
        }
    }
    public class ClearDuplicatesHandler:IRequestHandler<ClearDuplicates, Result>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;

        public ClearDuplicatesHandler(IPatientExtractRepository patientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
        }

        public async Task<Result> Handle(ClearDuplicates request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Manifest.UploadMode != UploadMode.Differential)
                {
                    Log.Debug("removing Site Duplicates...");
                    await _patientExtractRepository.RemoveDuplicates(request.Manifest.SiteCode);
                }
                return Result.Ok();
            }
            catch (Exception e)
            {
                Log.Error("Removing Site Duplicates Error", e);
                return Result.Fail(e.Message);
            }
        }
    }
}
