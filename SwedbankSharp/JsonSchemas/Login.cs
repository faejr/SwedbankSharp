using System;
using System.Collections.Generic;

namespace SwedbankSharp.JsonSchemas
{
    public class Login
    {

        public bool useEasyLogin {get; set;}
        public string password { get; set; }
        public bool generateEasyLoginId { get; set; }
        public long userId { get; set; }

    }

}
