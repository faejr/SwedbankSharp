using System;
using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class BaseInfo
    {
        public List<FromAccountGroup> fromAccountGroup { get; set; }
        public List<RecipientAccountGroup> recipientAccountGroup { get; set; }
        public List<string> perodicity { get; set; }
        public AddRecipientStatus addRecipientStatus { get; set; }
        public Links links { get; set; }
    }

    public class FromAccountGroup
    {
        public string name { get; set; }
        public string groupId { get; set; }
        public List<Account> accounts { get; set; }
    }

    public class BaseInfoAccount
    {
        public string balance { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string accountNumber { get; set; }
        public string clearingNumber { get; set; }
        public string fullyFormattedNumber { get; set; }
        public bool isDefault { get; set; }
        public List<object> availableForTags { get; set; }
    }

    public class RecipientAccountGroup
    {
        public string name { get; set; }
        public string groupId { get; set; }
        public List<BaseInfoAccount> accounts { get; set; }
    }

    public class NotAllowedMessage
    {
        public string message { get; set; }
        public string headline { get; set; }
    }

    public class AddRecipientStatus
    {
        public bool allowed { get; set; }
        public NotAllowedMessage notAllowedMessage { get; set; }
    }

}
