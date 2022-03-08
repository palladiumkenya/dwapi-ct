using AutoMapper;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Model.Profiles
{
   public class CtProfile : Profile
   {
       public CtProfile()
       {

           CreateMap<PatientSourceDto, StagePatientExtract>()
               .ForMember(dest => dest.PatientPID, opt => opt.MapFrom(src => src.PatientPK))
               .ForMember(dest => dest.PatientCccNumber, opt => opt.MapFrom(src => src.PatientID));
           CreateMap<VisitSourceDto, StageVisitExtract>();

           CreateMap<StagePatientExtract, PatientExtract>();
           CreateMap<StageVisitExtract, PatientVisitExtract>();
       }
   }
}
