using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.IO;
using CsvHelper;

namespace Lab5._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller cnt = new Controller();
            //cnt.createItemInShop("Шоколад", 39, 7, 3);
            cnt.MostCheapest("Шоколад");
            //cnt.MoreGoods(100, 4);
            Console.ReadKey();
        }
    }
}