using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using log4net;
using MediatR;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Commands
{
    public class ClearManifest:IRequest<Result>
    {
        public Manifest Manifest { get; }

        public ClearManifest(Manifest manifest)
        {
            Manifest = manifest;
        }
    }
    public class ClearManifestHandler:IRequestHandler<ClearManifest, Result>
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;

        public ClearManifestHandler(IPatientExtractRepository patientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
        }

        public async Task<Result> Handle(ClearManifest request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Debug("clearing Site Manifest...");
                await _patientExtractRepository.ClearManifest(request.Manifest);
                return Result.Ok();
            }
            catch (Exception e)
            {
                Log.Error("Clear Site Manifest Error", e);
                return Result.Fail(e.Message);
            }
        }
    }
}
