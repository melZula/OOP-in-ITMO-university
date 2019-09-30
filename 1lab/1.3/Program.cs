using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace _1._3
{
    class Program
    {
        static private int romeToInt(String romeValue)
        {
            Regex regex = new Regex("^(M{0,3})(D?C{0,3}|C[DM])(L?X{0,3}|X[LC])(V?I{0,3}|I[VX])$");
            MatchCollection matches = regex.Matches(romeValue);
            if (matches.Count > 0)
            {
                int intValue = 0;
                int last = 0;
                ArrayList romeList = new ArrayList();
                for (int i = 0; i <= 1000; i++)
                {
                    romeList.Add("0");
                }
                romeList.Insert(1, "I");
                romeList.Insert(5, "V");
                romeList.Insert(10, "X");
                romeList.Insert(50, "L");
                romeList.Insert(100, "C");
                romeList.Insert(500, "D");
                romeList.Insert(1000, "M");

                for (int i = romeValue.Length; i > 0; i--)
                {
                    String category = romeValue.Substring(i - 1, 1);
                    int lastValue = romeList.IndexOf(category);
                    if (lastValue < 1) { Console.WriteLine("Invalid symbol"); continue; }
                    if (lastValue >= last)
                    {
                        intValue += lastValue;
                    }
                    else
                    {
                        intValue -= lastValue;
                    }
                    last = lastValue;
                }

                return intValue;
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
                return 0;
            }
        }
        static void Main(string[] args)
        {
            int answ;
            answ = romeToInt(args[0]);
            Console.WriteLine(answ);
            Console.ReadKey();
        }
    }
}
