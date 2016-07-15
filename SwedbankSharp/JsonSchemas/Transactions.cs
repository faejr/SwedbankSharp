using System;
using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class TransactionList
    {
        public List<Transaction> Transactions { get; set; }
        public Account Account { get; set; }
        public int NumberOfTransactions { get; set; }
        public List<ReservedTransaction> ReservedTransactions { get; set; }
        public int NumberOfReservedTransactions { get; set; }
        public bool MoreTransactionsAvailable { get; set; }
        public Links2 Links { get; set; }
    }

    public class Transaction
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
    }

    public class Account
    {
        public string AvailableAmount { get; set; }
        public string CreditGranted { get; set; }
        public QuickbalanceSubscription QuickbalanceSubscription { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Currency { get; set; }
        public string AccountNumber { get; set; }
        public string ClearingNumber { get; set; }
        public string Balance { get; set; }
        public string FullyFormattedNumber { get; set; }
    }

    public class ReservedTransaction
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
    }
}
