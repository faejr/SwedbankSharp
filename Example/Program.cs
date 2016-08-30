using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SwedbankSharp;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            new AsyncExample().RunAsync().Wait();
        }
    }
}
