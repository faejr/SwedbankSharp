using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using RestSharp;

namespace SwedbankSharp
{
    public class Swedbank
    {

        private string s_baseUri = "https://auth.api.swedbank.se/TDE_DAP_Portal_REST_WEB/api/v1/";
        private string s_appID;
        private string s_useragent;
        private string s_authKey;
        private string s_dsid;
        private string s_selectedProfileID;
        private bool b_profileCorporate;
        private long l_id;
        private string s_password;
        private RestClient client;

        private Random random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Swedbank constructor, generates authkey and dsid
        /// </summary>
        /// <param name="id">Personal ID number</param>
        /// <param name="password">Personal code</param>
        /// <param name="bankType">Bank type (e.g. swedbank, sparbank etc)</param>
        public Swedbank(long id, string password, string bankType)
        {
            try
            {
                s_appID = BankType.Banks[bankType].AppId;
                s_useragent = BankType.Banks[bankType].UserAgent;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Incorrect bank type", ex);
            }

            s_authKey = GenerateAuthKey();
            s_dsid = Dsid();
            b_profileCorporate = (s_useragent.Contains("Corporate") ? true : false);
            l_id = id;
            s_password = password;
        }

        /// <summary>
        /// Generate authorization key
        /// </summary>
        /// <returns>Auth key</returns>
        private string GenerateAuthKey()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s_appID + ":" + Guid.NewGuid().ToString().ToUpper())); ;
        }

        /// <summary>
        /// Login to Swedbank
        /// </summary>
        /// <param name="id">Personal id number</param>
        /// <param name="password">Personal code</param>
        private bool Login()
        {
            JsonSchemas.Login ls = new JsonSchemas.Login();
            ls.useEasyLogin = false;
            ls.password = s_password;
            ls.generateEasyLoginId = false;
            ls.userId = l_id;

            JsonSchemas.PersonalCode t = SimpleJson.DeserializeObject<JsonSchemas.PersonalCode>(PostRequest("identification/personalcode", ls));
            if (t.PersonalCodeChangeRequired)
                throw new ApplicationException("Unable to login", new Exception("Personal code need to be changed"));

            if (t.links == null)
                return false;

            return true;
        }

        /// <summary>
        /// Creates a request (also sets up client)
        /// </summary>
        /// <param name="data">Data to send</param>
        /// <returns>A basic request</returns>
        private RestRequest CreateRequest(string uri, object data)
        {
            if (client == null)
            {
                client = new RestClient();
                client.BaseUrl = new Uri(s_baseUri);
                client.CookieContainer = new CookieContainer();
                client.AddDefaultHeader("Authorization", s_authKey);
                client.AddDefaultHeader("Accept", "*/*");
                client.AddDefaultHeader("Accept-Language", "sv-se");
                client.AddDefaultHeader("Accept-Encoding", "gzip, deflate");
                client.AddDefaultHeader("Keep-Alive", "true");
                client.UserAgent = s_useragent;
            }
            RestRequest request = new RestRequest();

            request.Resource = uri;

            request.AddCookie("dsid", s_dsid);

            request.AddJsonBody(data);

            request.AddQueryParameter("dsid", s_dsid);

            return request;
        }

        /// <summary>
        /// Creates and posts request
        /// </summary>
        /// <param name="uri">URI to send to</param>
        /// <param name="data">Data to send</param>
        /// <returns>Returned content</returns>
        private string PostRequest(string uri, object data)
        {
            RestRequest request = CreateRequest(uri, data);
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Unable to retrieve response", response.ErrorException);
            }

            return response.Content;
        }

        /// <summary>
        /// Creates a GET request
        /// </summary>
        /// <param name="uri">URI to send to</param>
        /// <param name="data">Data to send</param>
        /// <returns></returns>
        private string GetRequest(string uri, object data = null)
        {
            RestRequest request = CreateRequest(uri, data);
            request.Method = Method.GET;

            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Unable to retrieve response", response.ErrorException);
            }

            return response.Content;
        }

        /// <summary>
        /// Creates a PUT request
        /// </summary>
        /// <param name="uri">URI to send to</param>
        /// <param name="data">Data to send</param>
        /// <returns></returns>
        private string PutRequest(string uri, object data = null)
        {
            RestRequest request = CreateRequest(uri, data);
            request.Method = Method.PUT;

            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Unable to retrieve response", response.ErrorException);
            }

            return response.Content;
        }

        /// <summary>
        /// Logs out of the API
        /// </summary>
        public void Terminate()
        {
            PutRequest("identification/logout");
        }

        /// <summary>
        /// Shows account details and transaction for account
        /// </summary>
        /// <param name="accountId">Unique, random ID from Swedbank API</param>
        /// <param name="transactionsPerPage">Amount of transactions per page</param>
        /// <param name="page">Current page. (Requires transactionsPerPage)</param>
        /// <returns></returns>
        public JsonSchemas.Transactions AccountDetails(string accountId = "", int transactionsPerPage = 0, int page = 1)
        {
            if (String.IsNullOrEmpty(accountId))
                accountId = AccountList().transactionAccounts[0].id;

            JsonSchemas.TransactionQuery query = new JsonSchemas.TransactionQuery();
            query.transactionsPerPage = transactionsPerPage;
            query.page = page;

            JsonSchemas.Transactions output = SimpleJson.DeserializeObject<JsonSchemas.Transactions>(GetRequest("engagement/transactions/" + accountId, query));
            
            if(output.transactions == null)
                throw new ApplicationException("AccountID invalid.");

            return output;
        }

        /// <summary>
        /// List all bank accounts that are available for the profile. 
        /// If has been chosen, the first profile in the list is selected.
        /// </summary>
        /// <param name="profileID"></param>
        /// <returns>List of accounts</returns>
        public JsonSchemas.Overview AccountList(string profileID = "")
        {
            SelectProfile(profileID);

            JsonSchemas.Overview output = SimpleJson.DeserializeObject<JsonSchemas.Overview>(GetRequest("engagement/overview"));

            if(output.transactionAccounts == null)
                throw new ApplicationException("Unable to list bank accounts");

            return output;
        }

        /// <summary>
        /// Selects a profile
        /// </summary>
        /// <param name="profileID">Profile id to select</param>
        private void SelectProfile(string profileID = "")
        {
            if (String.IsNullOrEmpty(profileID))
            {
                if (!string.IsNullOrEmpty(s_selectedProfileID))
                    return;

                JsonSchemas.Bank profiles = ProfileList();
                if(b_profileCorporate)
                    throw new ApplicationException("Corporate profile not supported yet.");

                JsonSchemas.PrivateProfile profileData = profiles.privateProfile;
                profileID = profileData.id;
            }

            PostRequest("profile/" + profileID, null);

            s_selectedProfileID = profileID;

        }

        /// <summary>
        /// Profile information
        /// Access a list of profiles and each temporary ID-number. Every privateperson and corporation have their own profiles.
        /// </summary>
        /// <returns></returns>
        private JsonSchemas.Bank ProfileList()
        {
            if (client == null)
                Login();

            JsonSchemas.Profile output = SimpleJson.DeserializeObject<JsonSchemas.Profile>(GetRequest("profile/"));

            /*if (output.hasSavingbankProfile != null)
                throw new ApplicationException("Trouble getting profile");*/

            if (output.banks[0].bankId == null)
            {
                if (!output.hasSwedbankProfile && output.hasSavingbankProfile)
                    throw new ApplicationException("The account is not part of Swedbank. Choose a different BankID.");
                else if(output.hasSwedbankProfile && !output.hasSavingbankProfile)
                    throw new ApplicationException("The account is not part of Sparbanken. Choose a different BankID.");
                else
                    throw new ApplicationException("Profilepage contains no bank accounts.");
            }

            return output.banks[0];
        }

        /// <summary>
        /// Gets reminders such as unfulfilled payments
        /// </summary>
        /// <returns>Reminders</returns>
        private JsonSchemas.Reminders Reminders()
        {
            SelectProfile();

            return SimpleJson.DeserializeObject<JsonSchemas.Reminders>(GetRequest("message/reminders"));
        }

        /// <summary>
        /// Gets BaseInfo (Grouped accounts?)
        /// </summary>
        /// <returns>Reminders</returns>
        private JsonSchemas.BaseInfo BaseInfo()
        {
            SelectProfile();

            return SimpleJson.DeserializeObject<JsonSchemas.BaseInfo>(GetRequest("transfer/baseinfo"));
        }


        /// <summary>
        /// Get QuickBalanceAccounts
        /// </summary>
        /// <returns>List of accounts with quickbalance info</returns>
        public JsonSchemas.QuickBalanceAccounts QuickBalanceAccounts()
        {
            SelectProfile();

            return SimpleJson.DeserializeObject<JsonSchemas.QuickBalanceAccounts>(GetRequest("quickbalance/accounts"));
        }

        /// <summary>
        /// Returns the subscription id for quickbalance
        /// </summary>
        /// <param name="accountSubId"></param>
        /// <returns></returns>
        public string GetQuickBalanceSubscriptionId(string accountSubId)
        {
            JsonSchemas.QuickBalanceSubscriptionObject obj = SimpleJson.DeserializeObject<JsonSchemas.QuickBalanceSubscriptionObject>(PostRequest("quickbalance/subscription/"+accountSubId, null));
            return obj.subscriptionId;
        }

        /// <summary>
        /// Displays the quickbalance using given subscription id
        /// </summary>
        /// <param name="quickBalanceSubscriptionId"></param>
        /// <returns></returns>
        public JsonSchemas.QuickBalance QuickBalance(string quickBalanceSubscriptionId)
        {
            return SimpleJson.DeserializeObject<JsonSchemas.QuickBalance>(GetRequest("quickbalance/"+quickBalanceSubscriptionId));
        }

        /// <summary>
        /// Generate dsid for requests
        /// </summary>
        /// <returns>dsid string</returns>
        private string Dsid()
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
        public string ShuffleString(string stringToShuffle)
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
