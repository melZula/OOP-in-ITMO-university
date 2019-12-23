using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Linq;

namespace part1
{
    public class Data
    {
        public List<Item> all = new List<Item>();
        public Data()
        {
            using (var reader = new StreamReader("file.csv"))
            using (var csv = new CsvReader(reader))
            {
                all = csv.GetRecords<Item>().ToList();
            }
        }
    }
}