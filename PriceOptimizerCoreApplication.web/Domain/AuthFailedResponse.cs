using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceOptimizerCoreApplication.web.Domain
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
