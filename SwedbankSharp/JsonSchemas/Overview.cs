using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwedbankSharp.JsonSchemas
{
    public class Overview
    {
        public List<BankAccount> transactionAccounts { get; set; }
        public List<BankAccount> transactionDisposalAccounts { get; set; }
        public List<BankAccount> loanAccounts { get; set; }
        public List<BankAccount> savingAccounts { get; set; }
        public List<BankAccount> cardAccounts { get; set; }
        public CardCredit cardCredit { get; set; }
    }

    public class BankAccount
    {
        public bool selectedForQuickbalance { get; set; }
        public Links links { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string currency { get; set; }
        public string accountNumber { get; set; }
        public string clearingNumber { get; set; }
        public string balance { get; set; }
        public string fullyFormattedNumber { get; set; }
    }

    public class Next2
    {
        public string method { get; set; }
        public string uri { get; set; }
    }

    public class Links2
    {
        public Next2 next { get; set; }
    }

    public class CardCredit
    {
    }
}
