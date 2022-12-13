using PalladiumDwh.Core.Application.Extracts.Source.Dto;
using System.Collections.Generic;

namespace PalladiumDwh.Core.Application.Extracts.Source
{
    public class VisitSourceBag : SourceBag<VisitSourceDto>{
        public VisitSourceBag()
        {
        }

        public List<VisitSourceDto> _VisitSourceDto { get; set; }
        public VisitSourceBag(List<VisitSourceDto> visitSourceDto)
        {
            _VisitSourceDto = visitSourceDto;
        }
    }
}
