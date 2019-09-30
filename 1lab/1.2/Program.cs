using System;

namespace _1._2
{
    class Program
    {
        static void fibonachi(int n)
        {
            int first = 0;
            int second = 1;
            for (int  i = 0; i < (Convert.ToDecimal(n) / 2); i++)
            {
                Console.Write($"{first} ");
                if (n % 2 == 1 && i == n / 2) return;
                Console.Write($"{second} ");
                first += second;
                second += first;
            }
        }
        static void Main(string[] args)
        {
            int num;
            if (Int32.TryParse(args[0], out num) && num >0)
                fibonachi(Convert.ToInt32(num));
            else
                Console.WriteLine("Invalid input");

            Console.ReadKey();
        }
    }
}
