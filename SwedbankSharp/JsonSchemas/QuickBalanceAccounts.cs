using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwedbankSharp.JsonSchemas
{
    public class QuickBalanceAccounts
    {
        public List<QbAccount> accounts { get; set; }
    }

    public class QuickbalanceSubscription
    {
        public string id { get; set; }
        public bool active { get; set; }
        public Links links { get; set; }
    }

    public class QbAccount
    {
        public string name { get; set; }
        public string currency { get; set; }
        public string accountNumber { get; set; }
        public string clearingNumber { get; set; }
        public string balance { get; set; }
        public string fullyFormattedNumber { get; set; }
        public QuickbalanceSubscription quickbalanceSubscription { get; set; }
    }

}
