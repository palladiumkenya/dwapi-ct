using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class LaboratorySourceBag : SourceBag<LaboratorySourceDto>{
        public LaboratorySourceBag()
        {
        }

        public List<LaboratorySourceDto> _LaboratorySourceDto { get; set; }
        public LaboratorySourceBag(List<LaboratorySourceDto> laboratorySourceDto)
        {
            _LaboratorySourceDto = laboratorySourceDto;
        }
    }
}