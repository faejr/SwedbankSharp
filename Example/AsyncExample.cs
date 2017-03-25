using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwedbankSharp;

namespace Example
{
    public class AsyncExample
    {
        public async Task RunAsync()
        {
            Console.Write("Personal ID number: ");
            long idnumber = Convert.ToInt64(Console.ReadLine());

            SwedbankLogin client = new SwedbankLogin(BankType.Swedbank);

            await client.InitializeMobileBankIdLoginAsync(idnumber);

            bool loggedIn = false;
            Swedbank loggedInClient = null;
            while (!loggedIn)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

                Console.WriteLine("Waiting for bankid...");

                var status = await client.VerifyLoginAsync();
                loggedIn = status.LoggedIn;

                loggedInClient = status.Swedbank;
            }
            
            Console.WriteLine("\nRetrieving list of profiles...");
            var profile = await loggedInClient.GetProfileAsync();

            var privateProfileId = profile.Banks.First().PrivateProfile.Id; //Assume we have a private profile.
            await loggedInClient.SetProfileForSessionAsync(privateProfileId);

            var accounts = await loggedInClient.GetAccountListAsync();

            List<SwedbankSharp.JsonSchemas.BankAccount> bankAccounts = new List<SwedbankSharp.JsonSchemas.BankAccount>();
            bankAccounts.AddRange(accounts.TransactionAccounts);
            bankAccounts.AddRange(accounts.SavingAccounts);
            bankAccounts.AddRange(accounts.CardAccounts);

            int i = 1;
            foreach (SwedbankSharp.JsonSchemas.BankAccount ta in bankAccounts)
            {
                Console.WriteLine(i + ". " + ta.Name);
                i++;
            }

            Console.Write("Choose account: ");
            int no = ReadKey();

            var selectedAccount = bankAccounts[no - 1];
            Console.WriteLine("\nRetrieving account details...");

            var transactionList = await loggedInClient.GetAccountTransactionListAsync(selectedAccount.Id);

            Console.WriteLine("Account Name: " + transactionList.Account.Name);
            Console.WriteLine("Account Balance: " + transactionList.Account.Balance + transactionList.Account.Currency);

            await loggedInClient.TerminateAsync();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static int ReadKey()
        {
            while (true)
            {
                ConsoleKeyInfo choice = Console.ReadKey();
                Console.WriteLine();
                if (char.IsDigit(choice.KeyChar))
                {
                    int answer = Convert.ToInt32(choice.KeyChar);
                    return answer - 48;
                }
                Console.WriteLine("\nSorry, you need to input a number.");
            }
        }
    }
}
