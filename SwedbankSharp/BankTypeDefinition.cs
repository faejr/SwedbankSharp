using System.Collections.Generic;

namespace SwedbankSharp
{
    public class BankTypeDefinition
    {
        
        public static Dictionary<BankType, AppData> Banks = new Dictionary<BankType, AppData>();

        static BankTypeDefinition()
        {
            const string phoneInfo = "_(iOS;_9.2.1)_Apple/iPhone7,2";

            Banks.Add(BankType.Swedbank, new AppData() { AppId = "tKiUJOc0fAdy9itb", UserAgent = "SwedbankMOBPrivateIOS/4.5.0" + phoneInfo });
            Banks.Add(BankType.Sparbanken, new AppData() { AppId = "ApXJOPzxuClYQ09o", UserAgent = "SavingbankMOBPrivateIOS/3.9.0" + phoneInfo });
            Banks.Add(BankType.SwedbankUng, new AppData() { AppId = "SjH7oIgOqkGmqxUz", UserAgent = "SwedbankMOBYouthIOS/1.6.0" + phoneInfo });
            Banks.Add(BankType.SparbankenUng, new AppData() { AppId = "L9SJJQiYav1CvTtK", UserAgent = "SavingbankMOBYouthIOS/1.6.0" + phoneInfo });
            Banks.Add(BankType.SwedbankFöretag, new AppData() { AppId = "FXdVTYdzOGBvqe5l", UserAgent = "SwedbankMOBCorporateIOS/1.5.0" + phoneInfo, Corporate = true });
            Banks.Add(BankType.SparbankenFöretag, new AppData() { AppId = "SeUNIvpcNHnNPwvK", UserAgent = "SavingbankMOBCorporateIOS/1.5.0" + phoneInfo, Corporate = true });
        }

    }
}