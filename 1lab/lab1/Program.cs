using System;
using System.IO;
using System.Collections.Generic;

namespace lab1
{
    class Program
    {
        static private int[] data;
        static int[] reading()
        {
            try
            {
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    string line;
                    line = sr.ReadLine();
                    if (String.IsNullOrEmpty(line)) { return null; } // throw new ArgumentNullException();
                    string[] raw = line.Split(' ');
                    int[] arr = Array.ConvertAll<string, int>(raw, int.Parse);
                    return arr;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static void writing(int t)
        {
            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                sw.WriteLine(t);
            }
        }
        static int summ(int[] t)
        {
            int sum = 0;
            foreach(int n in t)
            {
                sum += n;
            }
            return sum;
        }
        static void Main(string[] args)
        {
            data = reading();
            if (data != null)
            {
                writing(summ(data));
            }
            else Console.WriteLine("No data");

            Console.ReadKey();
        }
    }
}
