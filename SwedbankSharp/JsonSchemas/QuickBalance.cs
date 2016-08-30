using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwedbankSharp.JsonSchemas
{
    public class QuickBalance
    {
        public string Currency { get; set; }
        public string Balance { get; set; }
        public bool BalanceForCustomer { get; set; }
        public bool RemindersExists { get; set; }
    }
}
