using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwedbankSharp.JsonSchemas
{
    public class LoginStatus
    {
        public string Status { get; set; }
        public bool ExtendedUsage { get; set; }
        public Links Links { get; set; }
        public string ServerTime { get; set; }
        public string FormattedServerTime { get; set; }
    }
}
