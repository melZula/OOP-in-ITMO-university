using System;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Linq;

namespace Lab5._2
{
        public class ShoppingCSV: IModel
    {
        public List<Shop> Shops { get ; set; }
        public List<Item> Items { get; set; }
        public List<Item_in_Shop> Item_in_Shop { get; set; }
        public ShoppingCSV()
        {
            using (var reader = new StreamReader(@"Shops.csv"))
            using (var csv = new CsvReader(reader))
            {
                Shops = csv.GetRecords<Shop>().ToList();
            }
            using (var reader = new StreamReader(@"Items.csv"))
            using (var csv = new CsvReader(reader))
            {
                Items = csv.GetRecords<Item>().ToList();
            }
            using (var reader = new StreamReader(@"Item_in_Shop.csv"))
            using (var csv = new CsvReader(reader))
            {
                //csv.Configuration.Delimiter = ",";
                Item_in_Shop = csv.GetRecords<Item_in_Shop>().ToList();
            }
        }
        public object add(Shop record)
        {
            using (var writer = new StreamWriter(@"Shops.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(record);
            }
            return null;
        }
        public object add(Item record)
        {
            using (var writer = new StreamWriter(@"Items.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(record);
            }
            return null;
        }
        public object add(Item_in_Shop record)
        {
            using (var writer = new StreamWriter(@"Item_in_Shop.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(record);
            }
            return null;
        }
        public object add(object a)
        {
            Console.WriteLine("ERROR");
            return null;
        }
        public void Dispose() { }
        public int SaveChanges()
        {
            return 1;
        }

    }

}