using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwedbankSharp.JsonSchemas
{
    public class QuickBalanceAccounts
    {
        public List<QbAccount> Accounts { get; set; }
    }

    public class QuickbalanceSubscription
    {
        public string Id { get; set; }
        public bool Active { get; set; }
        public Links Links { get; set; }
    }

    public class QbAccount
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public string AccountNumber { get; set; }
        public string ClearingNumber { get; set; }
        public string Balance { get; set; }
        public string FullyFormattedNumber { get; set; }
        public QuickbalanceSubscription QuickbalanceSubscription { get; set; }
    }

}
