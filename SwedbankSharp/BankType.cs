using System;
using System.Collections.Generic;

namespace SwedbankSharp
{
    public static class BankType
    {

        public static Dictionary<string, AppData> Banks = new Dictionary<string, AppData>();

        static BankType()
        {
            Banks.Add("swedbank", new AppData() { AppId = "HithYAGrzi8fu73j", UserAgent = "SwedbankMOBPrivateIOS/3.9.0_(iOS;_8.0.2)_Apple/iPhone5,2" });
            Banks.Add("sparbanken", new AppData() { AppId = "9iZSu74jfDFaTdPd", UserAgent = "SavingbankMOBPrivateIOS/3.9.0_(iOS;_8.0.2)_Apple/iPhone5,2" });
            Banks.Add("swedbank_ung", new AppData() { AppId = "IV4Wrt2VZtyYjfpW", UserAgent = "SwedbankMOBYouthIOS/1.6.0_(iOS;_8.0.2)_Apple/iPhone5,2" });
            Banks.Add("sparbanken_ung", new AppData() { AppId = "BrGkZQR89rEbFwnj", UserAgent = "SavingbankMOBYouthIOS/1.6.0_(iOS;_8.0.2)_Apple/iPhone5,2" });
            Banks.Add("swedbank_foretag", new AppData() { AppId = "v0RVbFGKMXz7U4Eb", UserAgent = "SwedbankMOBCorporateIOS/1.5.0_(iOS;_8.0.2)_Apple/iPhone5,2" });
            Banks.Add("sparbanken_foretag", new AppData() { AppId = "JPf1VxiskNdFSclr", UserAgent = "SavingbankMOBCorporateIOS/1.5.0_(iOS;_8.0.2)_Apple/iPhone5,2" });
        }

    }
}
