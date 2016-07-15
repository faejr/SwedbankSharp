using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class Overview
    {
        public List<BankAccount> TransactionAccounts { get; set; }
        public List<BankAccount> TransactionDisposalAccounts { get; set; }
        public List<BankAccount> LoanAccounts { get; set; }
        public List<BankAccount> SavingAccounts { get; set; }
        public List<BankAccount> CardAccounts { get; set; }
        public CardCredit CardCredit { get; set; }
    }

    public class BankAccount
    {
        public bool SelectedForQuickbalance { get; set; }
        public Links Links { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Currency { get; set; }
        public string AccountNumber { get; set; }
        public string ClearingNumber { get; set; }
        public string Balance { get; set; }
        public string FullyFormattedNumber { get; set; }
    }

    public class Next2
    {
        public string Method { get; set; }
        public string Uri { get; set; }
    }

    public class Links2
    {
        public Next2 Next { get; set; }
    }

    public class CardCredit
    {
    }
}
