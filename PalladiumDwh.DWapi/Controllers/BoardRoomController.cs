using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CSharpFunctionalExtensions;
using Hangfire;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PalladiumDwh.Core.Application.Commands;
using PalladiumDwh.Core.Application.Manifests.Commands;
using PalladiumDwh.Core.Application.Manifests.Queries;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using System.Web.Http;
using System.Web.Mvc;
using PalladiumDwh.DWapi.Models;
using System.Web;
using PalladiumDwh.Core.Application.Extracts.Source;
using System.Collections.Generic;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Core.Application.Extracts.Commands;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using PalladiumDwh.Shared.Model.Profile;
using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using AutoMapper;

namespace PalladiumDwh.DWapi.Controllers
{
    
    public class BoardRoomController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(Manifest).Name.ToLower();
        private readonly ILiveSyncService _liveSyncService;
        private readonly IFacilityRepository _facilityRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IMediator _mediator;
        

        public HttpClient Client { get; set; }

        public BoardRoomController(IMessagingSenderService messagingService,
          IPatientExtractRepository patientExtractRepository, ILiveSyncService liveSyncService,
          IFacilityRepository facilityRepository, IMediator mediator)
        {
            _messagingService = messagingService;
            _messagingService.Initialize(_gateway);
            _patientExtractRepository = patientExtractRepository;
            _liveSyncService = liveSyncService;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
            _serializerSettings = new JsonSerializerSettings()
            { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        
        [HttpPost]
        [Route("api/file/patients")]
        [Consumes("multipart/form-data")]
        public async Task<HttpResponseMessage> UploadFilePatients()
        {
            HttpPostedFile postedFile = System.Web.HttpContext.Current.Request.Files[0];
           
       
            MasterFacility masterFacility = null;
            string text;
            var client = Client ?? new HttpClient();
            int count = 0;
            int sendCound = 0;
           // var responses = new List<SendManifestResponse>();
            using (var stream = postedFile.InputStream)
            {
                var archive = new ZipArchive(stream);

                bool patientsProcessed = false;
                for (int i = 0; i < archive.Entries.Count; i++)
                {
                    if (archive.Entries[i].Name == "manifest.dump.json")
                    {                                                 
                            if (archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                            {
                                using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                                {
                                    try
                                    {
                                        text = await sr.ReadToEndAsync(); // OK                         

                                        byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                        var Extract = Encoding.UTF8.GetString(base64EncodedBytes);

                                            try
                                            {
                                                Manifest manifest = JsonConvert.DeserializeObject<Manifest>(Extract);


                                                if (null != manifest)
                                                {

                                                    #region Validate Site

                                                    masterFacility = await _mediator.Send(new GetValidFacility(manifest));

                                                    #endregion

                                                    #region Process Manifest

                                                    // job 1 Clear Manifest
                                                    var id1 = BatchJob.StartNew(x =>
                                                    {
                                                        x.Enqueue(() => SendManifest($"{manifest.Info("Clear")}", new ClearManifest(manifest)));
                                                    }, $"{manifest.Info("Clear")}");
                                                    // job 2 Removed Duplicates
                                                    var id2 = BatchJob.ContinueBatchWith(id1, async x =>
                                                    {
                                                        {
                                                           x.Enqueue(() => SendManifest($"{manifest.Info("Dedup")}", new ClearDuplicates(manifest)));
                                                        }
                                                    }, $"{manifest.Info("Dedup")}");
                                                    // job 3 Send to SPOT
                                                    var id3 = BatchJob.ContinueBatchWith(id2, x =>
                                                    {
                                                        {
                                                            x.Enqueue(() => SendManifest($"{manifest.Info("Spot")}", new SendToSpot(manifest, masterFacility)));
                                                        }
                                                    }, $"{manifest.Info("Spot")}");
                                                    // job 4 Sync Manifest
                                                    var jobId = BatchJob.ContinueBatchWith(id3, x =>
                                                    {
                                                        {
                                                            x.Enqueue(() => SendManifest($"{manifest.Info("Save")}",
                                                                new CreateManifest(manifest, masterFacility, Properties.Settings.Default.AllowSnapshot)));
                                                        }
                                                    }, $"{manifest.Info("Save")}");

                                                    #endregion

                                                    masterFacility.ManifestId = manifest.Id;
                                                    masterFacility.SessionId = manifest.Session;
                                                    masterFacility.JobId = jobId;                                                    
                                                }
                                                
                                            }
                                            catch (Exception e)
                                            {
                                            Log.Error(e);
                                          
                                            } 
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Error(e);
                                        throw;
                                    }
                                }
                            
                            }
                    }
                }
                for (int i = 1; i < archive.Entries.Count; i++)
                {
                    if (archive.Entries[i].Name == "PatientExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                            using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                            {
                                try
                                {
                                   
                                    text = await sr.ReadToEndAsync(); // OK                         

                                    byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                    var Extract = Encoding.UTF8.GetString(base64EncodedBytes);

                                    var options = new JsonSerializerOptions();
                                    options.Converters.Add(new JsonStringEnumConverter());
                               
                                    PatientSourceBag sourceBag = System.Text.Json.JsonSerializer.Deserialize<PatientSourceBag>(Extract, options);                                   
                                    var batchSize = 2000;
                                    var numberOfBatches = (int)Math.Ceiling((double)sourceBag.Extracts.Count() / batchSize); 
                                    var list= new List<PatientSourceBag>();                                    
                                    for (int x = 0; x < numberOfBatches; x++)
                                    {
                                        List<PatientSourceDto> newList = sourceBag.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                        list.Add(new PatientSourceBag(newList));                                                                               
                                    }
                                    foreach (var item in list)
                                        {
                                            sourceBag.Extracts = item._patientSourceDto;

                                            
                                            if (null != sourceBag && sourceBag.Extracts.Any())
                                            {
                                                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                                                {
                                                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                        new HttpError(
                                                            "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                                }
                                                try
                                                {

                                                    string jobId;
                                                    if (sourceBag.HasJobId)
                                                    {
                                                      jobId =  BatchJob.ContinueBatchWith(sourceBag.JobId,
                                                             x => {x.Enqueue(() =>SendP($"{sourceBag}", new SyncPatient(sourceBag))); }, $"{sourceBag}");
                                                            
                                                    }
                                                    
                                                    else
                                                    {
                                                        jobId = BatchJob.StartNew(x =>
                                                        {
                                                            x.Enqueue(() => SendP($"{sourceBag}", new SyncPatient(sourceBag)));
                                                        }, $"{sourceBag}");
                                                    }
                                       
                                            }
                                                catch (Exception ex)
                                                {
                                                    Log.Error(new string('*', 30));
                                                    Log.Error(nameof(PatientSourceBag), ex);
                                                    Log.Error(new string('*', 30));
                                                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                                }
                                            }                          
                                }
                               
                            }
                                catch (Exception ex)
                                {
                                        Log.Error(new string('*', 30));
                                        Log.Error(nameof(PatientSourceBag), ex);
                                        Log.Error(new string('*', 30));
                                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                            }
                            }
                    }
                    

                }                

            }
            return Request.CreateResponse(HttpStatusCode.OK, masterFacility);
        }

        [HttpPost]
        [Route("api/file")]
        [Consumes("multipart/form-data")]
        public async Task<HttpResponseMessage> UploadFile()
        {
            HttpPostedFile postedFile = System.Web.HttpContext.Current.Request.Files[0];


            MasterFacility masterFacility = null;
            string text;
            var client = Client ?? new HttpClient();
            int count = 0;
            int sendCound = 0;
            // var responses = new List<SendManifestResponse>();
            using (var stream = postedFile.InputStream)
            {
                var archive = new ZipArchive(stream);

                bool patientsProcessed = false;
                for (int i = 1; i < archive.Entries.Count; i++)
                {

                    if (archive.Entries[i].Name == "AllergiesChronicIllnessExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                AllergiesChronicIllnessSourceBag sourceBagallergy = System.Text.Json.JsonSerializer.Deserialize<AllergiesChronicIllnessSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagallergy.Extracts.Count() / batchSize);
                                var list = new List<AllergiesChronicIllnessSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<AllergiesChronicIllnessSourceDto> newList = sourceBagallergy.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new AllergiesChronicIllnessSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagallergy.Extracts = item._allergiesChronicIllnessSourceDto;
                                    if (null != sourceBagallergy && sourceBagallergy.Extracts.Any())
                                    {
                                        if (sourceBagallergy.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagallergy.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagallergy.JobId,
                                                    x => { x.Enqueue(() => SendAllergies($"{sourceBagallergy}", new SyncAllergiesChronicIllness(sourceBagallergy))); }, $"{sourceBagallergy}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendAllergies($"{sourceBagallergy}", new SyncAllergiesChronicIllness(sourceBagallergy)));
                                                }, $"{sourceBagallergy}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(AllergiesChronicIllnessSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }




                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    else if (archive.Entries[i].Name == "ContactListingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                ContactListingSourceBag sourceBagCL = System.Text.Json.JsonSerializer.Deserialize<ContactListingSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagCL.Extracts.Count() / batchSize);
                                var list = new List<ContactListingSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<ContactListingSourceDto> newList = sourceBagCL.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new ContactListingSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagCL.Extracts = item._ContactListingSourceDto;
                                    if (null != sourceBagCL && sourceBagCL.Extracts.Any())
                                    {
                                        if (sourceBagCL.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagCL.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagCL.JobId,
                                                    x => { x.Enqueue(() => SendContactListing($"{sourceBagCL}", new SyncContactListing(sourceBagCL))); }, $"{sourceBagCL}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendContactListing($"{sourceBagCL}", new SyncContactListing(sourceBagCL)));
                                                }, $"{sourceBagCL}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(ContactListingSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else if (archive.Entries[i].Name == "CovidExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {

                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                CovidSourceBag sourceBagCovid = System.Text.Json.JsonSerializer.Deserialize<CovidSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagCovid.Extracts.Count() / batchSize);
                                var list = new List<CovidSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<CovidSourceDto> newList = sourceBagCovid.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new CovidSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagCovid.Extracts = item._CovidSourceDto;
                                    if (null != sourceBagCovid && sourceBagCovid.Extracts.Any())
                                    {
                                        if (sourceBagCovid.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagCovid.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagCovid.JobId,
                                                    x => { x.Enqueue(() => SendCovid($"{sourceBagCovid}", new SyncCovid(sourceBagCovid))); }, $"{sourceBagCovid}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendCovid($"{sourceBagCovid}", new SyncCovid(sourceBagCovid)));
                                                }, $"{sourceBagCovid}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(CovidSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else if (archive.Entries[i].Name == "DefaulterTracingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {

                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());

                                DefaulterTracingSourceBag sourceBagDT = System.Text.Json.JsonSerializer.Deserialize<DefaulterTracingSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagDT.Extracts.Count() / batchSize);
                                var list = new List<DefaulterTracingSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<DefaulterTracingSourceDto> newList = sourceBagDT.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new DefaulterTracingSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagDT.Extracts = item._DefaulterTracingSourceDto;
                                    if (null != sourceBagDT && sourceBagDT.Extracts.Any())
                                    {
                                        if (sourceBagDT.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagDT.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagDT.JobId,
                                                    x => { x.Enqueue(() => SendDefaultTracing($"{sourceBagDT}", new SyncDefaulterTracing(sourceBagDT))); }, $"{sourceBagDT}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendDefaultTracing($"{sourceBagDT}", new SyncDefaulterTracing(sourceBagDT)));
                                                }, $"{sourceBagDT}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(DefaulterTracingSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }


                        }
                    }
                    else if (archive.Entries[i].Name == "DepressionScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());

                                DepressionScreeningSourceBag sourceBagDS = System.Text.Json.JsonSerializer.Deserialize<DepressionScreeningSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagDS.Extracts.Count() / batchSize);
                                var list = new List<DepressionScreeningSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<DepressionScreeningSourceDto> newList = sourceBagDS.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new DepressionScreeningSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagDS.Extracts = item._DepressionScreeningSourceDto;
                                    if (null != sourceBagDS && sourceBagDS.Extracts.Any())
                                    {
                                        if (sourceBagDS.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagDS.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagDS.JobId,
                                                    x => { x.Enqueue(() => SendDepressionScreening($"{sourceBagDS}", new SyncDepressionScreening(sourceBagDS))); }, $"{sourceBagDS}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendDepressionScreening($"{sourceBagDS}", new SyncDepressionScreening(sourceBagDS)));
                                                }, $"{sourceBagDS}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(DepressionScreeningSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                    }
                    else if (archive.Entries[i].Name == "DrugAlcoholScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                DrugAlcoholScreeningSourceBag sourceBagDrugAlcoholScreening = System.Text.Json.JsonSerializer.Deserialize<DrugAlcoholScreeningSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagDrugAlcoholScreening.Extracts.Count() / batchSize);
                                var list = new List<DrugAlcoholScreeningSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<DrugAlcoholScreeningSourceDto> newList = sourceBagDrugAlcoholScreening.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new DrugAlcoholScreeningSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagDrugAlcoholScreening.Extracts = item._DrugAlcoholScreeningSourceDto;
                                    if (null != sourceBagDrugAlcoholScreening && sourceBagDrugAlcoholScreening.Extracts.Any())
                                    {
                                        if (sourceBagDrugAlcoholScreening.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagDrugAlcoholScreening.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagDrugAlcoholScreening.JobId,
                                                    x => { x.Enqueue(() => SendDrugAlcohol($"{sourceBagDrugAlcoholScreening}", new SyncDrugAlcoholScreening(sourceBagDrugAlcoholScreening))); }, $"{sourceBagDrugAlcoholScreening}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendDrugAlcohol($"{sourceBagDrugAlcoholScreening}", new SyncDrugAlcoholScreening(sourceBagDrugAlcoholScreening)));
                                                }, $"{sourceBagDrugAlcoholScreening}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(DrugAlcoholScreeningSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                    else if (archive.Entries[i].Name == "EnhancedAdherenceCounsellingExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                EnhancedAdherenceCounsellingSourceBag sourceBagEnhancedAdherance = System.Text.Json.JsonSerializer.Deserialize<EnhancedAdherenceCounsellingSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagEnhancedAdherance.Extracts.Count() / batchSize);
                                var list = new List<EnhancedAdherenceCounsellingSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<EnhancedAdherenceCounsellingSourceDto> newList = sourceBagEnhancedAdherance.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new EnhancedAdherenceCounsellingSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagEnhancedAdherance.Extracts = item._EnhancedAdherenceCounsellingSourceDto;
                                    if (null != sourceBagEnhancedAdherance && sourceBagEnhancedAdherance.Extracts.Any())
                                    {
                                        if (sourceBagEnhancedAdherance.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagEnhancedAdherance.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagEnhancedAdherance.JobId,
                                                    x => { x.Enqueue(() => SendEnhancedAdherance($"{sourceBagEnhancedAdherance}", new SyncEnhancedAdherenceCounselling(sourceBagEnhancedAdherance))); }, $"{sourceBagEnhancedAdherance}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendEnhancedAdherance($"{sourceBagEnhancedAdherance}", new SyncEnhancedAdherenceCounselling(sourceBagEnhancedAdherance)));
                                                }, $"{sourceBagEnhancedAdherance}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(EnhancedAdherenceCounsellingSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                    else if (archive.Entries[i].Name == "GbvScreeningExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                GbvScreeningSourceBag sourceBagGbvScreening = System.Text.Json.JsonSerializer.Deserialize<GbvScreeningSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagGbvScreening.Extracts.Count() / batchSize);
                                var list = new List<GbvScreeningSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<GbvScreeningSourceDto> newList = sourceBagGbvScreening.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new GbvScreeningSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagGbvScreening.Extracts = item._GbvScreeningSourceDto;
                                    if (null != sourceBagGbvScreening && sourceBagGbvScreening.Extracts.Any())
                                    {
                                        if (sourceBagGbvScreening.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagGbvScreening.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagGbvScreening.JobId,
                                                    x => { x.Enqueue(() => SendGbvScreening($"{sourceBagGbvScreening}", new SyncGbvScreening(sourceBagGbvScreening))); }, $"{sourceBagGbvScreening}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendGbvScreening($"{sourceBagGbvScreening}", new SyncGbvScreening(sourceBagGbvScreening)));
                                                }, $"{sourceBagGbvScreening}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(GbvScreeningSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else if (archive.Entries[i].Name == "IptExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());

                                IptSourceBag sourceBagItv = System.Text.Json.JsonSerializer.Deserialize<IptSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagItv.Extracts.Count() / batchSize);
                                var list = new List<IptSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<IptSourceDto> newList = sourceBagItv.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new IptSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagItv.Extracts = item._IptSourceDto;
                                    if (null != sourceBagItv && sourceBagItv.Extracts.Any())
                                    {
                                        if (sourceBagItv.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagItv.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagItv.JobId,
                                                    x => { x.Enqueue(() => SendIpt($"{sourceBagItv}", new SyncIpt(sourceBagItv))); }, $"{sourceBagItv}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendIpt($"{sourceBagItv}", new SyncIpt(sourceBagItv)));
                                                }, $"{sourceBagItv}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(IptSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    else if (archive.Entries[i].Name == "OtzExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                OtzSourceBag sourceBagOtz = System.Text.Json.JsonSerializer.Deserialize<OtzSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagOtz.Extracts.Count() / batchSize);
                                var list = new List<OtzSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<OtzSourceDto> newList = sourceBagOtz.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new OtzSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagOtz.Extracts = item._OtzSourceDto;
                                    if (null != sourceBagOtz && sourceBagOtz.Extracts.Any())
                                    {
                                        if (sourceBagOtz.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagOtz.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagOtz.JobId,
                                                    x => { x.Enqueue(() => SendOtz($"{sourceBagOtz}", new SyncOtz(sourceBagOtz))); }, $"{sourceBagOtz}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendOtz($"{sourceBagOtz}", new SyncOtz(sourceBagOtz)));
                                                }, $"{sourceBagOtz}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(OtzSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else if (archive.Entries[i].Name == "OvcExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                OvcSourceBag sourceBagOvc = System.Text.Json.JsonSerializer.Deserialize<OvcSourceBag>(Extract, options);

                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagOvc.Extracts.Count() / batchSize);
                                var list = new List<OvcSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<OvcSourceDto> newList = sourceBagOvc.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new OvcSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagOvc.Extracts = item._OvcSourceDto;
                                    if (null != sourceBagOvc && sourceBagOvc.Extracts.Any())
                                    {
                                        if (sourceBagOvc.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagOvc.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagOvc.JobId,
                                                    x => { x.Enqueue(() => SendOvc($"{sourceBagOvc}", new SyncOvc(sourceBagOvc))); }, $"{sourceBagOvc}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendOvc($"{sourceBagOvc}", new SyncOvc(sourceBagOvc)));
                                                }, $"{sourceBagOvc}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(OvcSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                    }
                    else if (archive.Entries[i].Name == "PatientAdverseEventExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                AdverseEventSourceBag sourceBagAdverseEvent = System.Text.Json.JsonSerializer.Deserialize<AdverseEventSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagAdverseEvent.Extracts.Count() / batchSize);
                                var list = new List<AdverseEventSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<AdverseEventSourceDto> newList = sourceBagAdverseEvent.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new AdverseEventSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagAdverseEvent.Extracts = item._AdverseEventSourceDto;
                                    if (null != sourceBagAdverseEvent && sourceBagAdverseEvent.Extracts.Any())
                                    {
                                        if (sourceBagAdverseEvent.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagAdverseEvent.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagAdverseEvent.JobId,
                                                    x => { x.Enqueue(() => SendPatientAdverse($"{sourceBagAdverseEvent}", new SyncAdverseEvent(sourceBagAdverseEvent))); }, $"{sourceBagAdverseEvent}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendPatientAdverse($"{sourceBagAdverseEvent}", new SyncAdverseEvent(sourceBagAdverseEvent)));
                                                }, $"{sourceBagAdverseEvent}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(AdverseEventSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    else if (archive.Entries[i].Name == "PatientArtExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                ArtSourceBag sourceBagArt = System.Text.Json.JsonSerializer.Deserialize<ArtSourceBag>(Extract, options);

                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagArt.Extracts.Count() / batchSize);
                                var list = new List<ArtSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<ArtSourceDto> newList = sourceBagArt.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new ArtSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagArt.Extracts = item._ArtSourceDto;
                                    if (null != sourceBagArt && sourceBagArt.Extracts.Any())
                                    {
                                        if (sourceBagArt.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagArt.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagArt.JobId,
                                                    x => { x.Enqueue(() => SendArt($"{sourceBagArt}", new SyncArt(sourceBagArt))); }, $"{sourceBagArt}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendArt($"{sourceBagArt}", new SyncArt(sourceBagArt)));
                                                }, $"{sourceBagArt}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(ArtSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    else if (archive.Entries[i].Name == "PatientBaselineExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());

                                BaselineSourceBag sourceBagBaseline = System.Text.Json.JsonSerializer.Deserialize<BaselineSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagBaseline.Extracts.Count() / batchSize);
                                var list = new List<BaselineSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<BaselineSourceDto> newList = sourceBagBaseline.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new BaselineSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagBaseline.Extracts = item._BaselineSourceDto;
                                    if (null != sourceBagBaseline && sourceBagBaseline.Extracts.Any())
                                    {
                                        if (sourceBagBaseline.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagBaseline.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagBaseline.JobId,
                                                    x => { x.Enqueue(() => SendBaselines($"{sourceBagBaseline}", new SyncBaseline(sourceBagBaseline))); }, $"{sourceBagBaseline}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendBaselines($"{sourceBagBaseline}", new SyncBaseline(sourceBagBaseline)));
                                                }, $"{sourceBagBaseline}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(BaselineSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    else if (archive.Entries[i].Name == "PatientLabExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());

                                LaboratorySourceBag sourceBagLab = System.Text.Json.JsonSerializer.Deserialize<LaboratorySourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagLab.Extracts.Count() / batchSize);
                                var list = new List<LaboratorySourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<LaboratorySourceDto> newList = sourceBagLab.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new LaboratorySourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagLab.Extracts = item._LaboratorySourceDto;
                                    if (null != sourceBagLab && sourceBagLab.Extracts.Any())
                                    {
                                        if (sourceBagLab.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagLab.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagLab.JobId,
                                                    x => { x.Enqueue(() => SendLabs($"{sourceBagLab}", new SyncLaboratory(sourceBagLab))); }, $"{sourceBagLab}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendLabs($"{sourceBagLab}", new SyncLaboratory(sourceBagLab)));
                                                }, $"{sourceBagLab}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(LaboratorySourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }

                    else if (archive.Entries[i].Name == "PatientPharmacyExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                PharmacySourceBag sourceBagPharmacy = System.Text.Json.JsonSerializer.Deserialize<PharmacySourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagPharmacy.Extracts.Count() / batchSize);
                                var list = new List<PharmacySourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<PharmacySourceDto> newList = sourceBagPharmacy.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new PharmacySourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagPharmacy.Extracts = item._PharmacySourceDto;
                                    if (null != sourceBagPharmacy && sourceBagPharmacy.Extracts.Any())
                                    {
                                        if (sourceBagPharmacy.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {

                                            string jobId;
                                            if (sourceBagPharmacy.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagPharmacy.JobId,
                                                    x => { x.Enqueue(() => SendPharmacy($"{sourceBagPharmacy}", new SyncPharmacy(sourceBagPharmacy))); }, $"{sourceBagPharmacy}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendPharmacy($"{sourceBagPharmacy}", new SyncPharmacy(sourceBagPharmacy)));
                                                }, $"{sourceBagPharmacy}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(PharmacySourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    else if (archive.Entries[i].Name == "PatientStatusExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         

                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                StatusSourceBag sourceBagStatus = System.Text.Json.JsonSerializer.Deserialize<StatusSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagStatus.Extracts.Count() / batchSize);
                                var list = new List<StatusSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<StatusSourceDto> newList = sourceBagStatus.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new StatusSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagStatus.Extracts = item._StatusSourceDto;
                                    if (null != sourceBagStatus && sourceBagStatus.Extracts.Any())
                                    {
                                        if (sourceBagStatus.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {
                                            string jobId;
                                            if (sourceBagStatus.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagStatus.JobId,
                                                    x => { x.Enqueue(() => SendStatus($"{sourceBagStatus}", new SyncStatus(sourceBagStatus))); }, $"{sourceBagStatus}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendStatus($"{sourceBagStatus}", new SyncStatus(sourceBagStatus)));
                                                }, $"{sourceBagStatus}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(StatusSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }
                    else if (archive.Entries[i].Name == "PatientVisitExtract.dump.json" && archive.Entries[i].FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(archive.Entries[i].Open()))
                        {
                            try
                            {
                                text = await sr.ReadToEndAsync(); // OK                         
                                string jobId;
                                byte[] base64EncodedBytes = Convert.FromBase64String(text);
                                var Extract = Encoding.UTF8.GetString(base64EncodedBytes);


                                var options = new JsonSerializerOptions();
                                options.Converters.Add(new JsonStringEnumConverter());
                                VisitSourceBag sourceBagVisit = System.Text.Json.JsonSerializer.Deserialize<VisitSourceBag>(Extract, options);
                                var batchSize = 2000;
                                var numberOfBatches = (int)Math.Ceiling((double)sourceBagVisit.Extracts.Count() / batchSize);
                                var list = new List<VisitSourceBag>();
                                for (int x = 0; x < numberOfBatches; x++)
                                {
                                    List<VisitSourceDto> newList = sourceBagVisit.Extracts.Skip(x * batchSize).Take(batchSize).ToList();
                                    list.Add(new VisitSourceBag(newList));
                                }
                                foreach (var item in list)
                                {
                                    sourceBagVisit.Extracts = item._VisitSourceDto;
                                    if (null != sourceBagVisit && sourceBagVisit.Extracts.Any())
                                    {
                                        if (sourceBagVisit.Extracts.Any(x => !x.IsValid()))
                                        {
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                new HttpError(
                                                    "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                                        }

                                        try
                                        {


                                            if (sourceBagVisit.HasJobId)
                                            {
                                                jobId = BatchJob.ContinueBatchWith(sourceBagVisit.JobId,
                                                    x => { x.Enqueue(() => SendVisits($"{sourceBagVisit}", new SyncVisit(sourceBagVisit))); }, $"{sourceBagVisit}");
                                            }
                                            else
                                            {
                                                jobId = BatchJob.StartNew(x =>
                                                {
                                                    x.Enqueue(() => SendVisits($"{sourceBagVisit}", new SyncVisit(sourceBagVisit)));
                                                }, $"{sourceBagVisit}");
                                            }

                                            //return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                                            //    new
                                            //    {
                                            //        JobId = jobId,
                                            //        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                                            //    });
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error(new string('*', 30));
                                            Log.Error(nameof(VisitSourceBag), ex);
                                            Log.Error(new string('*', 30));
                                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }

            }
            return Request.CreateResponse(HttpStatusCode.OK, masterFacility);
        }



        [Queue("omega")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendEnhancedAdherance(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("omega")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendGbvScreening(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }


        [Queue("gamma")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendDrugAlcohol(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }



        [Queue("delta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendDepressionScreening(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }


        [Queue("alpha")]       
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendP(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
        [Queue("alpha")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendManifest(string jobName, IRequest<Result> command)
        {
            await _mediator.Send(command);
        }
        [Queue("omega")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendAllergies(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
        [Queue("delta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendContactListing(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
        [Queue("gamma")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendCovid(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
        [Queue("gamma")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendDefaultTracing(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("omega")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendIpt(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
        [Queue("delta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendOtz(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("delta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendOvc(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("beta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendPatientAdverse(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("beta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendArt(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("beta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendBaselines(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
        [Queue("beta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendLabs(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
        [Queue("beta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendPharmacy(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("beta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendStatus(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }

        [Queue("beta")]
        // [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SendVisits(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }


    }
}
