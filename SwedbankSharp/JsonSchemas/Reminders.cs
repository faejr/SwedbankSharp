namespace SwedbankSharp.JsonSchemas
{
    public class Reminders
    {
        public RejectedPayments RejectedPayments { get; set; }
        public UnsignedPayments UnsignedPayments { get; set; }
        public UnsignedTransfers UnsignedTransfers { get; set; }
        public IncomingEinvoices IncomingEinvoices { get; set; }
    }

    public class RejectedPayments
    {
        public Links Links { get; set; }
        public string Count { get; set; }
    }

    public class UnsignedPayments
    {
        public Links2 Links { get; set; }
        public string Count { get; set; }
    }

    public class Next3
    {
        public string Method { get; set; }
        public string Uri { get; set; }
    }

    public class Links3
    {
        public Next3 Next { get; set; }
    }

    public class UnsignedTransfers
    {
        public Links3 Links { get; set; }
        public string Count { get; set; }
    }

    public class Next4
    {
        public string Method { get; set; }
        public string Uri { get; set; }
    }

    public class Links4
    {
        public Next4 Next { get; set; }
    }

    public class IncomingEinvoices
    {
        public Links4 Links { get; set; }
        public string Count { get; set; }
    }

}
