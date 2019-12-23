using System;
using System.Reflection;

namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            Context ct = new Context();
            Item item = data.all[0];
            string mode = "size";

            if (mode == "name")
                { ct.setStrategy(new sortByName() ); }
            if (mode == "id")
                { ct.setStrategy(new sortById() ); }
            if (mode == "color")
                { ct.setStrategy(new sortByColor() ); }
            if (mode == "size")
                { ct.setStrategy(new sortBySize() ); }

            ct.executeStrategy(data);

            System.Console.WriteLine("Bye");
        }
    }
}
