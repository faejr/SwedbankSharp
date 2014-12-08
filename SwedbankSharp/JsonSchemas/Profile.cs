using System;
using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class Profile
    {
        public string userId { get; set; }
        public bool hasSwedbankProfile { get; set; }
        public bool hasSavingbankProfile { get; set; }
        public List<Bank> banks { get; set; }
    }

    public class Next
    {
        public string method { get; set; }
        public string uri { get; set; }
    }

    public class Edit
    {
        public string method { get; set; }
        public string uri { get; set; }
    }

    public class Links
    {
        public Next next { get; set; }
        public Edit edit { get; set; }
    }

    public class PrivateProfile
    {
        public string id { get; set; }
        public string bankId { get; set; }
        public string customerName { get; set; }
        public string customerNumber { get; set; }
        public Links links { get; set; }
    }

    public class Bank
    {
        public string name { get; set; }
        public string bankId { get; set; }
        public PrivateProfile privateProfile { get; set; }
        public List<object> corporateProfiles { get; set; }
    }
}
