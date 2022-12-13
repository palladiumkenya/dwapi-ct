using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PalladiumDwh.DWapi.Models
{
    public class FileUploadModel
    {
        public IFormFile file { get; set; }
    }
}