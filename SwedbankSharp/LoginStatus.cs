using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwedbankSharp
{
    public class LoginStatus
    {
        public Swedbank Swedbank { get; set; }
        public bool LoggedIn { get; set; }
        public string LoginState { get; set; }
    }
}
