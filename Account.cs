using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Account
    {
        private string accountNumber;
        private double balance;
        private string type;
        int onePartOfNumberMin = 10;
        int onePartOfNumberMax = 99;

        public static Random randomAcc = new Random(Guid.NewGuid().GetHashCode());

        public double Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
        public string AccountNumber
        {
            get
            {
                return accountNumber; 
            }
            
            set
            {
                accountNumber = value;
            }
        }
        public Account()
        {
            for (int i = 1; i <= 13; i++)
            {
                this.accountNumber += randomAcc.Next(onePartOfNumberMin, onePartOfNumberMax).ToString();
            }
            this.balance = 0;
            this.type = "normal";
        }

        public Account(string type, double balance)
        {
            for (int i = 1; i <= 13; i++)
            {
                this.accountNumber += randomAcc.Next(onePartOfNumberMin, onePartOfNumberMax).ToString();
            }
            this.balance = balance;
            this.type = type;
        }

        public static void DisplayAllAccounts(List <Account> allAccounts)
        {
            allAccounts.ToArray();
            Console.WriteLine("ALL ACCOUNTS INFORMATION");
            foreach (var item in allAccounts)
            {
                Console.WriteLine(item.accountNumber);
                Console.WriteLine(item.type);
                Console.WriteLine(item.balance);

            }
        }
    }
}
