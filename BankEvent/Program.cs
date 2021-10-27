using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            const int TRANSACTIONS = 5;
            char code;
            decimal amt;

            BankAccount acct = new BankAccount(334455); // 4567543   4567544
            EventListener listener = new EventListener(acct);

            for(int i = 0; i < TRANSACTIONS; i++)
            {
                Console.Write("Enter D for deposit or W for withdrawl: ");
                code = Convert.ToChar(Console.ReadLine());
                Console.Write("Enter dollar amount: ");
                amt = Convert.ToDecimal(Console.ReadLine());

                if (code == 'D')
                    acct.MakeDeposit(amt);
                if (code == 'W')
                    acct.MakeWithdrawl(amt);

                if (code != 'D' && code != 'W')
                    Console.WriteLine("Invalid operation");
            }

            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        } 
    } // end class Program

    class BankAccount
    {
        private int acctNum;
        private decimal balance;
        public event EventHandler BalanceAdjusted;

        public BankAccount(int acct)
        {
            acctNum = acct;
            balance = 0;
        }

        public int AcctNum
        {
            get { return acctNum; }
        }

        public decimal Balance
        {
            get { return balance; }
        }

        public void MakeDeposit(decimal amt)
        {
            balance += amt;
            OnBalanceAdjusted(EventArgs.Empty);
        }

        public void MakeWithdrawl(decimal amt)
        {
            balance -= amt;
            OnBalanceAdjusted(EventArgs.Empty);
        }

        public void OnBalanceAdjusted(EventArgs e)
        {
            BalanceAdjusted(this, e);
        }
    } // end class BankAccount

    class EventListener
    {
        public BankAccount acct;

        public EventListener(BankAccount account)
        {
            acct = account;
            acct.BalanceAdjusted += new EventHandler(BankAccountBalanceAdjusted);
        }

        private void BankAccountBalanceAdjusted(object sender, EventArgs e)
        {
            Console.WriteLine("The account balance has been adjusted.");
            Console.WriteLine("Account# {0} balance {1}",
                acct.AcctNum, acct.Balance.ToString("C2"));
        }
    } // end class EventListener
}
