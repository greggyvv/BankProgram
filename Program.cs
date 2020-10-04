using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        // FIELDS OF A PROGRAM
        private static int clientIDcheck = 0;
        private static int clientLoginCount = 1;
        private static string clientEmailChange = String.Empty;
        private static string clientPasswordcheck = String.Empty;
        public static Client[] databaseClients = new Client[1];
        public static List<Account> allAccounts = ReturnAllAccounts();

        public static List<Account> ReturnAllAccounts()
        {
            // ACCOUNTS
            Account acc1 = new Account();
            Account acc2 = new Account("business", 15000);
            Account acc3 = new Account("normal", 0);
            Account acc4 = new Account("business", 10000);
            Account acc5 = new Account("normal", 500);
            Account acc6 = new Account("business", 15000);
            Account acc7 = new Account("normal", 1000);

            List<Account> allAccounts = new List<Account>();
            allAccounts.Add(acc1);
            allAccounts.Add(acc2);
            allAccounts.Add(acc3);
            allAccounts.Add(acc4);
            allAccounts.Add(acc5);
            allAccounts.Add(acc6);
            allAccounts.Add(acc7);

            return allAccounts;
        }
        public static void SetUpVariablesAndDatabase()
        {
            // CLIENT
            Client client1 = new Client("password123", "Dawid", "Siemiaszko", "1993-10-27", "dsiemiaszko@gmail.com");
            Program.databaseClients[0] = client1;

            // CLIENT ADDED 2 ACCOUNTS
            client1.accountsList.Add(allAccounts[0]);
            client1.accountsList.Add(allAccounts[1]);
        }

        public static void LogIn()
        {
            bool loggedIn = false;
            Console.WriteLine("Welcome to the Bank. You have chosen an option \"Client\".\n");
            Console.WriteLine("---------------");
            Console.WriteLine("Please type your Client ID");

            // WE DISPLAY CLIENT ID ONLY FOR QUICK TESTING
            Console.WriteLine(databaseClients[0].ClientID);
        LoginStart:
            clientIDcheck = 0;
            while (clientIDcheck == 0)
            {
                try
                {
                    clientIDcheck = Convert.ToInt32(Console.ReadLine());
                    if (clientIDcheck <= 0)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Client ID should be a number >0.");
                    continue;
                }
            }

            for (int i = 0; i < databaseClients.Length; i++)
            {
                while (loggedIn == false)
                {
                    if (databaseClients[i].ClientID == clientIDcheck)
                    {
                        loggedIn = true;
                        // SETTING ACTIVE CLIENT VARIABLE
                        var activeClient = databaseClients[i];
                        Console.WriteLine("Correct Client ID, the chosen Client ID is: " + clientIDcheck);
                        Console.WriteLine("Please type a password");

                        // CHECK PASSWORD
                        while (clientPasswordcheck == String.Empty)
                        {
                            clientPasswordcheck = Console.ReadLine();
                            if (activeClient.ClientPassword == clientPasswordcheck)
                            {
                                // SUCCESSFULLY LOGGED IN
                                clientPasswordcheck = String.Empty;

                                Console.WriteLine("Correct password. You logged in.");
                                ChooseOptionAfterLogin(activeClient);
                            }
                            // WRONG PASSWORD HANDLING
                            else if (activeClient.ClientPassword != clientPasswordcheck && clientLoginCount != 3)
                            {
                                Console.WriteLine("Wrong password. Please type the correct password. Login attempts: " + clientLoginCount);
                                clientLoginCount++;
                                clientPasswordcheck = String.Empty;
                                continue;
                            }
                            // TOO MUCH LOGIN ATTEMPTS, ACCOUNT BLOCKED
                            else
                            {
                                Console.WriteLine("Login attempts: " + clientLoginCount + " , ACCOUNT BLOCKED. End of program.");
                            }
                        }
                    }
                    else
                    {
                        // INCORRECT CLIENT ID. WE GO BACK TO THE BEGINNING OF THE PROGRAM
                        Console.WriteLine("Incorrect Client ID. Try again.");
                        loggedIn = false;
                        goto LoginStart;
                    }
                }
            }
        }
        public static void DisplayClientCockpit(Client activeClient)
        {
            Console.WriteLine("---------------");
            Console.WriteLine("YOU ARE LOGGED AS: " + activeClient.firstName + " " + activeClient.secondName);
            Console.WriteLine("YOUR CLIENT ID: " + activeClient.ClientID);
            Console.WriteLine("YOUR E-MAIL ADDRESS: " + activeClient.ClientEmail);
            Console.WriteLine("YOUR ACCOUNTS: ");
            Console.WriteLine("---------------");

            // DISPLAYING CLIENT'S OPTIONS
            Console.WriteLine($"8. \tChange e-mail address\n9.\tLog out");

            // DISPLAYING CLIENT'S ACCOUNTS
            for (int j = 0; j < activeClient.accountsList.Count; j++)
            {
                Console.WriteLine($"{j + 1}.\tAccount number = {activeClient.accountsList[j].AccountNumber}\n\tType = {activeClient.accountsList[j].Type}\n\tBalance = {activeClient.accountsList[j].Balance} EUR\n");
            }
            // NOW CHOOSE THE OPTION: CHANGE PASSWORD, LOG OUT OR CHOOSE YOUR ACCOUNT TO OPERATE ON
            Console.WriteLine("Choose the operation. Change e-mail/password or operate on the accounts.");
        }

        public static int ChoosingOptionCatchingException(Client activeClient)
        {
            int chosenOption = 0;
            while (chosenOption == 0)
            {
                try
                {
                    chosenOption = Convert.ToInt32(Console.ReadLine());
                    if (chosenOption < 1 || chosenOption > activeClient.accountsList.Count && chosenOption < 8 || chosenOption == 0 || chosenOption > 9)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Choose the account you want to operate on. Or 8/9 for change your password/log out.");
                    chosenOption = 0;
                    continue;
                }
            }
            return chosenOption;
        }

        public static void LogoutOption()
        {
            Console.WriteLine("You logged out. End of the program.");
        }

        public static void ChangeEmailOption(Client activeClient)
        {
        ChangingEmail:
            int changingEmailOption = 0;
            clientPasswordcheck = String.Empty;
            Console.WriteLine("You want to change your e-mail address, please type the old password first");
            while (clientPasswordcheck == String.Empty)
            {
                Console.WriteLine("Please type the correct password to change your e-mail address");
                clientPasswordcheck = Console.ReadLine();
                if (activeClient.ClientPassword == clientPasswordcheck)
                {
                    Console.WriteLine("Correct password");
                    activeClient.ChangeEmail(activeClient);
                    ChooseOptionAfterLogin(activeClient);
                }
                else
                {
                    Console.WriteLine("Wrong password");
                    clientPasswordcheck = String.Empty;
                WrongPasswordWhenChangingEmail:
                    Console.WriteLine("1. Try again\n2. Go back");
                    try
                    {
                        changingEmailOption = Convert.ToInt32(Console.ReadLine());
                        if (changingEmailOption != 1 && changingEmailOption != 2)
                        {
                            throw new FormatException();
                        }
                        else
                        {
                            if (changingEmailOption == 1)
                            {
                                goto ChangingEmail;
                            }
                            else
                            {
                                ChooseOptionAfterLogin(activeClient);
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please type 1 or 2");
                        goto WrongPasswordWhenChangingEmail;
                    }
                }
            }
        }
        public static void ChooseOptionAfterLogin(Client activeClient)
        {
            DisplayClientCockpit(activeClient);

            int chosenOption = ChoosingOptionCatchingException(activeClient);
            // CHANGE EMAIL OPTION
            if (chosenOption == 8)
            {
                ChangeEmailOption(activeClient);
            }
            // LOG OUT OPTION
            else if (chosenOption == 9)
            {
                LogoutOption();
            }
            // OPERATING ON THE ACCOUNT
            else
            {
                OperatingOnAccount(activeClient, chosenOption);
            }
        }
        // OPERATIONS AFTER CHOSE THE ACCOUNT CATCHING EXCEPTION
        public static int OptionsOfAccountCatchingException()
        {
            int chosenOperation = 0;
            while (chosenOperation == 0)
            {
                Console.WriteLine("Please choose an option from 1 to 3.");
                try
                {
                    chosenOperation = Convert.ToInt32(Console.ReadLine());
                    if (chosenOperation < 1 || chosenOperation > 3)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong identifier. You should type number [1-3]");
                    chosenOperation = 0;
                    continue;
                }
            }
            return chosenOperation;
        }
        // DEPOSIT MONEY METHOD
        public static void DepositMoney(Client activeClient, Account activeAccount, int chosenOption)
        {
            double amountToDeposit = 0;
            while (amountToDeposit == 0)
            {
                Console.WriteLine("Please how much money do you want to deposit.");
                try
                {
                    amountToDeposit = Convert.ToDouble(Console.ReadLine());
                    if (amountToDeposit <= 0)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please provide an amount of money you want to deposit. Provide a number > 0");
                    amountToDeposit = 0;
                    continue;
                }
            }
            activeClient.DepositMoney(activeAccount, amountToDeposit);
            OperatingOnAccount(activeClient, chosenOption);
        }
        // TRANSFER MONEY METHOD
        public static void TransferMoney(Client activeClient, Account activeAccount, int chosenOption)
        {
            string accNumberToTransfer = String.Empty;
            double amountToTransfer = 0;
            while (accNumberToTransfer == String.Empty)
            {
                Console.WriteLine("Please type an account number where you want to transfer money: ");
                try
                {
                    accNumberToTransfer = Console.ReadLine();
                    if (accNumberToTransfer.Length != 26)
                    {
                        throw new FormatException();
                    }

                    for (int m = 0; m < allAccounts.Count; m++)
                    {
                        if (allAccounts[m].AccountNumber == accNumberToTransfer)
                        {
                            break;
                        }
                        else if (allAccounts[m].AccountNumber != accNumberToTransfer)
                        {
                            Console.WriteLine("Wpisane = " + accNumberToTransfer);
                            Console.WriteLine("Pobrane = " + allAccounts[m].AccountNumber);
                            Console.WriteLine("There is no such account. Please try again");
                            accNumberToTransfer = String.Empty;
                            break;
                        }
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Please provide 26-digit account number where you want to transfer the money.");
                    accNumberToTransfer = String.Empty;
                    continue;
                }
            }

            while (amountToTransfer == 0)
            {
                Console.WriteLine($"Please type an amount that you want to transfer to account no. {accNumberToTransfer}");
                try
                {
                    amountToTransfer = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Amount should be a number. Please type again the amount to transfer.");
                    amountToTransfer = 0;
                    continue;
                }
            }

            activeClient.MoneyTransfer(activeAccount, accNumberToTransfer, amountToTransfer, allAccounts);
            Console.WriteLine("xxxxxxxxxxx");
            OperatingOnAccount(activeClient, chosenOption);
        }
        // ACTIVE ACCOUNT COCKPIT METHOD
        public static void OperatingOnAccount(Client activeClient, int chosenOption)
        {
            // WE HAVE ACTIVE ACCOUNT
            var activeAccount = activeClient.accountsList[chosenOption - 1];
            Console.WriteLine($"You chose an account with number: {activeAccount.AccountNumber}");
            Console.WriteLine($"Balance = {activeAccount.Balance}");

            // CHOOSE OPERATION
            Console.WriteLine("1. Deposit money");
            Console.WriteLine("2. Make a transfer");
            Console.WriteLine("3. Go back");

            int chosenOperation = OptionsOfAccountCatchingException();


            // SWITCH FOR OPERATIONS
            switch (chosenOperation)
            {
                // DEPOSIT
                case 1:
                    DepositMoney(activeClient, activeAccount, chosenOption);
                    break;
                // TRANSFER    
                case 2:
                    TransferMoney(activeClient, activeAccount, chosenOption);
                    break;


                // GO BACK
                case 3:
                    ChooseOptionAfterLogin(activeClient);
                    break;
            }
        }

       
        static void Main(string[] args)
        {
            SetUpVariablesAndDatabase();
            LogIn();
        }
    }
}
