using System;
using System.Collections.Generic;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank Sberban = new Bank(_procent: 0.09, _commission: 0.01);
            Bank Tink = new Bank(_procent: 0.06, _commission: 0.01);
            
            Client Petya = new Client("Petya", "Pupkin", "some address", "32345535434");
            Client Vasya = new Client("Vasya", "Pupkin", "another address", "f323244");

            DebetAccount da = new DebetAccount(Petya, Sberban);
            CreditAccount ca = new CreditAccount(Vasya, Tink, -5000);

            da.money += 10000;
            ca.money = 5000;
            da.show();
            Sberban.accounts.Add(da);
            Tink.accounts.Add(ca);
            while (Console.ReadLine() != "0")
            {
                System.Console.WriteLine("{0}", Time.Get());
                Time.nextDay();
                Sberban.update();
                Tink.update();
            }
        }
    }
    class Time
    {
        static private DateTime _time = DateTime.Now;
        static public DateTime Get()
        {
            return _time;
        }
        static public void nextDay()
        {
            _time = _time.AddDays(1);
        }
    }
}
