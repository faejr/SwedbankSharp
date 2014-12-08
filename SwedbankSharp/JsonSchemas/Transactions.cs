using System;
using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class Transactions
    {
        public List<Transaction> transactions { get; set; }
        public Account account { get; set; }
        public int numberOfTransactions { get; set; }
        public List<ReservedTransaction> reservedTransactions { get; set; }
        public int numberOfReservedTransactions { get; set; }
        public bool moreTransactionsAvailable { get; set; }
        public Links2 links { get; set; }
    }

    public class Transaction
    {
        public string date { get; set; }
        public string description { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
    }

    public class QuickbalanceSubscription
    {
        public string id { get; set; }
        public bool active { get; set; }
        public Links links { get; set; }
    }

    public class Account
    {
        public string availableAmount { get; set; }
        public string creditGranted { get; set; }
        public QuickbalanceSubscription quickbalanceSubscription { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string currency { get; set; }
        public string accountNumber { get; set; }
        public string clearingNumber { get; set; }
        public string balance { get; set; }
        public string fullyFormattedNumber { get; set; }
    }

    public class ReservedTransaction
    {
        public string date { get; set; }
        public string description { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
    }

}
