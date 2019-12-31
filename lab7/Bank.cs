using System.Collections.Generic;
using System;

namespace lab7
{
    class Bank
    {
        public List<Client> clients = new List<Client>();
        public List<Account> accounts = new List<Account>();
        public double procent;
        public double commission;
        public void addClient()
        {
            System.Console.Write("Enter name > ");
            string name = Console.ReadLine();
            System.Console.Write("Enter surname > ");
            string surname = Console.ReadLine();
            System.Console.Write("Enter adress > ");
            string adress = Console.ReadLine();
            System.Console.Write("Enter passport > ");
            string passportNo = Console.ReadLine();
            clients.Add(new Client(name, surname, adress, passportNo));
        }
        public void update()
        {
            foreach (Account acc in accounts)
            {
                acc.update();
                acc.show();
            }
        }
        public Bank(double _procent, double _commission)
        {
            procent = _procent; commission = _commission;
        }
    }
}