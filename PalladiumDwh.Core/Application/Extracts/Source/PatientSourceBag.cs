using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class PatientSourceBag :SourceBag<PatientSourceDto> {

        public PatientSourceBag()
        {
        }
       
        public List<PatientSourceDto> _patientSourceDto { get; set; }
        public PatientSourceBag(List<PatientSourceDto> patientSourceDto)
        {
            _patientSourceDto = patientSourceDto;
        }


    }
}
