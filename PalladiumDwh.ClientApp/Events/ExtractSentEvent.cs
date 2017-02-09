using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientApp.Events
{
    public class ExtractSentEvent: EventArgs
    {
        public List<ExtractSetting> Extracts { get; private set; }

        public ExtractSentEvent(List<ExtractSetting> extracts)
        {
            Extracts = extracts;
        }
    }
}