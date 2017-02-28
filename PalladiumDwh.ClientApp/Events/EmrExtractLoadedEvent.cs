using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Events
{
    public class EmrExtractLoadedEvent: EventArgs
    {
        public List<ExtractSetting> Extracts { get; private set; }
        
        public EmrExtractLoadedEvent(List<ExtractSetting> extracts)
        {
            Extracts = extracts;
        }
    }
}