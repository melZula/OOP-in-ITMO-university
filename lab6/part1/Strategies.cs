using System;
using System.Collections.Generic;
using System.Linq;

namespace part1
{
    class sortByName: IStrategy
    {
        public List<Item> execute(Data data)
        {
            System.Console.WriteLine("Sort by name");
            return data.all.OrderBy(x => x.name).ToList();
        }
    }
    class sortById: IStrategy
    {
        public List<Item> execute(Data data)
        {
            System.Console.WriteLine("Sort by id");
            return data.all.OrderBy(x => x.id).ToList();
        }
    }
    class sortByColor: IStrategy
    {
        public List<Item> execute(Data data)
        {
            System.Console.WriteLine("Sort by color");
            return data.all.OrderBy(x => x.color).ToList();
        }
    }
    class sortBySize: IStrategy
    {
        public List<Item> execute(Data data)
        {
            System.Console.WriteLine("Sort by size");
            return data.all.OrderBy(x => x.size).ToList();
        }
    }
}