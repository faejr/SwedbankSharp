using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwedbankSharp.JsonSchemas
{
    public class QuickBalance
    {
        public string currency { get; set; }
        public string balance { get; set; }
        public bool balanceForCustomer { get; set; }
        public bool remindersExists { get; set; }
    }
}
