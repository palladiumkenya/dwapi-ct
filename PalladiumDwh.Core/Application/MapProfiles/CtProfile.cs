using AutoMapper;
using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Application.MapProfiles
{
   public class CtProfile : Profile
   {
       public CtProfile()
       {

           CreateMap<PatientSourceDto, StagePatientExtract>()
               .ForMember(dest => dest.PatientPID, opt => opt.MapFrom(src => src.PatientPK))
               .ForMember(dest => dest.PatientCccNumber, opt => opt.MapFrom(src => src.PatientID));

           CreateMap<VisitSourceDto, StageVisitExtract>();
           CreateMap<AdverseEventSourceDto, StageAdverseEventExtract>();
           CreateMap<AllergiesChronicIllnessSourceDto, StageAllergiesChronicIllnessExtract>();
           CreateMap<ArtSourceDto, StageArtExtract>();
           CreateMap<BaselineSourceDto, StageBaselineExtract>();
           CreateMap<ContactListingSourceDto, StageContactListingExtract>();
           CreateMap<CovidSourceDto, StageCovidExtract>();
           CreateMap<DefaulterTracingSourceDto, StageDefaulterTracingExtract>();
           CreateMap<DepressionScreeningSourceDto, StageDepressionScreeningExtract>();
           CreateMap<DrugAlcoholScreeningSourceDto, StageDrugAlcoholScreeningExtract>();
           CreateMap<EnhancedAdherenceCounsellingSourceDto, StageEnhancedAdherenceCounsellingExtract>();
           CreateMap<IptSourceDto, StageIptExtract>();
           CreateMap<LaboratorySourceDto, StageLaboratoryExtract>();
           CreateMap<OtzSourceDto, StageOtzExtract>();
           CreateMap<OvcSourceDto, StageOvcExtract>();
           CreateMap<PharmacySourceDto, StagePharmacyExtract>();
           CreateMap<StatusSourceDto, StageStatusExtract>();
           CreateMap<GbvScreeningSourceDto, StageGbvScreeningExtract>();
           CreateMap<CancerScreeningSourceDto, StageCancerScreeningExtract>();
           CreateMap<IITRiskScoresSourceDto, StageIITRiskScoresExtract>();
           CreateMap<ArtFastTrackSourceDto, StageArtFastTrackExtract>();
           CreateMap<CervicalCancerScreeningSourceDto, StageCervicalCancerScreeningExtract>();
           CreateMap<RelationshipsSourceDto, StageRelationshipsExtract>();

           CreateMap<StagePatientExtract, PatientExtract>();
           CreateMap<StageVisitExtract, PatientVisitExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageAdverseEventExtract, PatientAdverseEventExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageAllergiesChronicIllnessExtract, AllergiesChronicIllnessExtract>()
               .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageArtExtract, PatientArtExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageBaselineExtract, PatientBaselinesExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageContactListingExtract, ContactListingExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageCovidExtract, CovidExtract>()
               .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageDefaulterTracingExtract, DefaulterTracingExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageDepressionScreeningExtract, DepressionScreeningExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageDrugAlcoholScreeningExtract, DrugAlcoholScreeningExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageEnhancedAdherenceCounsellingExtract, EnhancedAdherenceCounsellingExtract>()
               .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageIptExtract, IptExtract>()
               .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageLaboratoryExtract, PatientLaboratoryExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageOtzExtract, OtzExtract>()
               .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageOvcExtract, OvcExtract>()
               .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StagePharmacyExtract, PatientPharmacyExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageStatusExtract, PatientStatusExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageGbvScreeningExtract, GbvScreeningExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageCancerScreeningExtract, CancerScreeningExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageIITRiskScoresExtract, IITRiskScoresExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageArtFastTrackExtract, ArtFastTrackExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageCervicalCancerScreeningExtract, CervicalCancerScreeningExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));
           CreateMap<StageRelationshipsExtract, RelationshipsExtract>().ForMember(dest => dest.PatientId,
               opt => opt.MapFrom(src => src.CurrentPatientId));

       }
   }
}
