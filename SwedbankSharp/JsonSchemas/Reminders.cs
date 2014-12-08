using System;
using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class Reminders
    {
        public RejectedPayments rejectedPayments { get; set; }
        public UnsignedPayments unsignedPayments { get; set; }
        public UnsignedTransfers unsignedTransfers { get; set; }
        public IncomingEinvoices incomingEinvoices { get; set; }
    }

    public class RejectedPayments
    {
        public Links links { get; set; }
        public string count { get; set; }
    }

    public class UnsignedPayments
    {
        public Links2 links { get; set; }
        public string count { get; set; }
    }

    public class Next3
    {
        public string method { get; set; }
        public string uri { get; set; }
    }

    public class Links3
    {
        public Next3 next { get; set; }
    }

    public class UnsignedTransfers
    {
        public Links3 links { get; set; }
        public string count { get; set; }
    }

    public class Next4
    {
        public string method { get; set; }
        public string uri { get; set; }
    }

    public class Links4
    {
        public Next4 next { get; set; }
    }

    public class IncomingEinvoices
    {
        public Links4 links { get; set; }
        public string count { get; set; }
    }

}
