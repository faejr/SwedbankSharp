using System;
using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class Profile
    {
        public string UserId { get; set; }
        public bool HasSwedbankProfile { get; set; }
        public bool HasSavingbankProfile { get; set; }
        public List<Bank> Banks { get; set; }
    }

    public class Next
    {
        public string Method { get; set; }
        public string Uri { get; set; }
    }

    public class Edit
    {
        public string Method { get; set; }
        public string Uri { get; set; }
    }

    public class Links
    {
        public Next Next { get; set; }
        public Edit Edit { get; set; }
    }

    public class PrivateProfile
    {
        public string Id { get; set; }
        public string BankId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public Links Links { get; set; }
    }

    public class Bank
    {
        public string Name { get; set; }
        public string BankId { get; set; }
        public PrivateProfile PrivateProfile { get; set; }
        public List<object> CorporateProfiles { get; set; }
    }
}
