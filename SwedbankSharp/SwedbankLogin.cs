using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SwedbankSharp
{
    public class SwedbankLogin
    {
        private readonly AppData _selectedBank;
        private readonly SwedbankRequester _requester;
        
        public SwedbankLogin(BankType bank)
        {
            _selectedBank = BankTypeDefinition.Banks[bank];

            _requester = new SwedbankRequester(GenerateAuthKey(_selectedBank), GenerateDsid(), _selectedBank.UserAgent);
        }

        public async Task InitializeMobileBankIdLoginAsync(long personnummer)
        {
            var response = await _requester.PostAsync("identification/bankid/mobile", new JsonSchemas.Login()
            {
                UseEasyLogin = false,
                GenerateEasyLoginId = false,
                UserId = personnummer
            });

            response.EnsureSuccessStatusCode();
        }
        
        public async Task<LoginStatus> VerifyLoginAsync()
        {
            var apiStatus = await _requester.GetAsync<JsonSchemas.LoginStatus>("identification/bankid/mobile/verify");

            if (apiStatus.Status == "COMPLETE")
            {
                return new LoginStatus()
                {
                    LoggedIn = true,
                    LoginState = "COMPLETE",
                    Swedbank = new Swedbank(_selectedBank, _requester)
                };
            }
            return new LoginStatus()
            {
                LoggedIn = false,
                LoginState = apiStatus.Status
            };
        }

        /// <summary>
        /// Generate authorization key
        /// </summary>
        /// <returns>Auth key</returns>
        private string GenerateAuthKey(AppData bank)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(bank.AppId + ":" + Guid.NewGuid().ToString().ToUpper())); ;
        }

        /// <summary>
        /// Generate dsid for requests
        /// </summary>
        /// <returns>dsid string</returns>
        private string GenerateDsid()
        {
            string dsid = RandomString(8);
            dsid = dsid.Substring(0, 4) + dsid.Substring(4, 4).ToUpper();
            return ShuffleString(dsid);
        }

        /// <summary>
        /// Generate a random string
        /// </summary>
        /// <param name="size">How long of a string?</param>
        /// <returns></returns>
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString().ToLower();
        }

        /// <summary>
        /// Shuffle all letters in a string
        /// </summary>
        /// <param name="stringToShuffle">String to shuffle</param>
        /// <returns></returns>
        private string ShuffleString(string stringToShuffle)
        {
            if (String.IsNullOrEmpty(stringToShuffle))
            {
                throw new ArgumentNullException("stringToShuffle",
                                                "The stringToShuffle variable must not be null or empty");
            }

            return new string(
                                 stringToShuffle
                                    .OrderBy(character => Guid.NewGuid())
                                    .ToArray()
                            );
        }
    }
}
