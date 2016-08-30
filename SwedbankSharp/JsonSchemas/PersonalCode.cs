using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwedbankSharp.JsonSchemas
{
    public class PersonalCode
    {
        public bool PersonalCodeChangeRequired { get; set; }
        public string ServerTime { get; set; }
        public Links Links { get; set; }
    }
}
