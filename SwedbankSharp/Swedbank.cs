using System;
using System.Threading.Tasks;

namespace SwedbankSharp
{
    public class Swedbank
    {
        private readonly AppData _selectedBank;
        private readonly SwedbankRequester _requester;
        private string _currentProfile = null;

        public Swedbank(AppData selectedBank, SwedbankRequester requester)
        {
            _selectedBank = selectedBank;
            _requester = requester;
        }

        /// <summary>
        /// Logs out of the API
        /// </summary>
        public async Task TerminateAsync()
        {
            await _requester.PutAsync("identification/logout");
        }

        /// <summary>
        /// Shows account details and transaction for account
        /// </summary>
        /// <param name="accountId">Unique ID for account in current session from Swedbank API</param>
        /// <returns></returns>
        public async Task<JsonSchemas.TransactionList> GetAccountTransactionListAsync(string accountId)
        {
            VerifyProfileIsSet();
            
            JsonSchemas.TransactionList transactionListList = await _requester.GetAsync<JsonSchemas.TransactionList>("engagement/transactions/" + accountId);
            
            return transactionListList;
        }

        public async Task SetProfileForSessionAsync(string profileId)
        {
            var response = await _requester.PostAsync("profile/" + profileId);
            response.EnsureSuccessStatusCode();

            _currentProfile = profileId; //Potential race condition.
        }

        /// <summary>
        /// List all bank accounts that are available for the current profile.
        /// </summary>
        /// <returns>List of accounts</returns>
        public async Task<JsonSchemas.Overview> GetAccountListAsync()
        {
            VerifyProfileIsSet();

            JsonSchemas.Overview output = await _requester.GetAsync<JsonSchemas.Overview>("engagement/overview");

            if(output.TransactionAccounts == null)
                throw new ApplicationException("Unable to list bank accounts");

            return output;
        }

        private void VerifyProfileIsSet()
        {
            if (_currentProfile == null)
                throw new Exception("Profile not selected");
        }

        /// <summary>
        /// Profile information
        /// Access a list of profiles and each temporary ID-number. Every privateperson and corporation have their own profiles.
        /// </summary>
        /// <returns></returns>
        public async Task<JsonSchemas.Profile> GetProfileAsync()
        {
            JsonSchemas.Profile output = await _requester.GetAsync<JsonSchemas.Profile>("profile/");
            
            return output;
        }

        /// <summary>
        /// Gets reminders such as unfulfilled payments
        /// </summary>
        /// <returns>Reminders</returns>
        public async Task<JsonSchemas.Reminders> GetRemindersAsync()
        {
            VerifyProfileIsSet();

            return await _requester.GetAsync<JsonSchemas.Reminders>("message/reminders");
        }

        /// <summary>
        /// Gets BaseInfo (Grouped accounts?)
        /// </summary>
        /// <returns>Reminders</returns>
        public async Task<JsonSchemas.BaseInfo> GetBaseInfoAsync()
        {
            VerifyProfileIsSet();

            return await _requester.GetAsync<JsonSchemas.BaseInfo>("transfer/baseinfo");
        }
    }
}
