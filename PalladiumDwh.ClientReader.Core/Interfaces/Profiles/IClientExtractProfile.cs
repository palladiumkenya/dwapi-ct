using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Profiles
{
    public interface IClientExtractProfile
    {
        ClientFacilityDTO Facility { get; set; }
        ClientPatientExtractDTO Demographic { get; set; }
        string EndPoint { get; set; }
    }
}