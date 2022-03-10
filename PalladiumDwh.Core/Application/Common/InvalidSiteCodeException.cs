using System;

namespace PalladiumDwh.Core.Application.Common
{
    public class InvalidSiteCodeException:Exception
    {
        public InvalidSiteCodeException(string message) : base(message)
        {

        }
    }
}
