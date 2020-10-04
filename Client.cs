using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bank
{
    class Client : Person
    {
        private int clientID;
        public List<Account> accountsList = new List<Account>();
        private string clientPassword;
        private static Random randomID = new Random(Guid.NewGuid().GetHashCode());

        public Client(string clientPassword, string firstName, string secondName, string dateOfBirth, string email)
        {
            this.clientID = randomID.Next(1000000);
            this.clientPassword = clientPassword;
            this.firstName = firstName;
            this.secondName = secondName;
            this.dateOfBirth = dateOfBirth;
            this.email = email;
        }

        // ACCESSORS
        public int ClientID
        {
            get
            {
                return clientID;
            }

            set
            {
                clientID = value;
            }
        }

        public string ClientPassword
        {
            get
            {
                return clientPassword;
            }

            set
            {
                clientPassword = value;
            }
        }

        public string ClientEmail
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }


        // MONEY TRANSFER METHOD
        public void MoneyTransfer(Account activeAccount, string accountNumber, double amount, List<Account> allAccounts)
        {
            if (activeAccount.Balance >= amount)
            {
                for (int i = 0; i < allAccounts.Count; i++)
                {
                    if (accountNumber == allAccounts[i].AccountNumber)
                    {
                        allAccounts[i].Balance += amount;
                        Console.WriteLine($"You transfer {amount} EUR from your account to account with number {allAccounts[i].AccountNumber}");
                    }
                }
                activeAccount.Balance -= amount;
            } else if(amount < 0)
            {
                Console.WriteLine("Amount that you want to transfer should be >0");
            } else
            {
                Console.WriteLine("You dont have enough money.");
            }

        }

        // DEPOSIT MONEY METHOD
        public void DepositMoney(Account activeAccount, double amount)
        {
            activeAccount.Balance += amount;
            Console.WriteLine($"You deposit money to your account, amount: {amount} EUR");
        }

        // CHANGE PASSWORD METHOD
        public void EditPassword(string newPassword)
        {

        }

        // CHANGE EMAIL METHOD
        public void ChangeEmail(Client activeClient)
        {
            Console.WriteLine("Please type new e-mail address");
            string clientNewEmail = String.Empty;
            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            while (clientNewEmail == String.Empty)
            {
                try
                {
                    clientNewEmail = Console.ReadLine();
                    Match match = emailRegex.Match(clientNewEmail);
                    if (!match.Success)
                    {
                        clientNewEmail = String.Empty;
                        throw new FormatException();
                    }
                    else
                    {
                        activeClient.email = clientNewEmail;
                        Console.WriteLine("Successfully changed e-mail. New e-mail address: "+activeClient.email);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please type correct e-mail address [xxx@yyy.zzz]");
                    continue;
                }
            }
        }

        public void RemoveAccount(long accountID, string password)
        {

        }

        public void CreateAccount(string password)
        {

        }
    }
}
