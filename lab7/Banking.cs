using System.Collections.Generic;
using System.Linq;

namespace lab7
{
    class Banking
    {
        static int _uniq = 1;
        public static int uniq 
        {
            get { _uniq++; return _uniq; }
        }
        static public List<Bank> banks = new List<Bank>();
        static public List<transact> transactions = new List<transact>();
        static public void AddBank(double proc, double com)
        {
            banks.Add(new Bank(proc, com));
        }
        static public void AddBank(Bank bank)
        {
            banks.Add(bank);
        }
        static void sendMoney(Account s, Account r, double sum)
        {
            transactions.Add(new transact(s, r, sum));
        }
    }
    struct transact
    {
        public Account sender;
        public Account recipient;
        public double money;
        public bool status;
        public transact(Account _send, Account _rec, double _mon)
        {
            sender = _send; recipient = _rec; money = _mon;
            if (sender.money - money > 0)
            {
                sender.money -= money;
                recipient.money += money;
                status = true;
                System.Console.WriteLine("Transaction commited");
            }
            else
            {
                status = false;
                System.Console.WriteLine("Not enough money");
            }
        }
        public void rollback()
        {
            if (recipient.money - money > 0)
            {
                recipient.money -= money;
                sender.money += money;
                status = false;
                System.Console.WriteLine("Rollback");
            }
        }
    }
}