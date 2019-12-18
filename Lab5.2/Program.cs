using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.IO;
using CsvHelper;

namespace Lab5
{
    public interface IModel: IDisposable
    {
        public List<Shop> Shops { get; set; }
        public List<Item> Items { get; set; }
        public List<Item_in_Shop> Item_in_Shop { get; set; }
        public object add(object a);
        public object add(Shop a);
        public object add(Item a);
        public object add(Item_in_Shop a);
        public int SaveChanges();
    }
    public class ShoppingDB : DbContext, IModel
    {
        public List<Shop> Shops { get => _Shops.ToList<Shop>(); set { _Shops.AddRange(value); } }
        public List<Item> Items { get => _Items.ToList<Item>(); set { _Items.AddRange(value); } }
        public List<Item_in_Shop> Item_in_Shop { get => _Item_In_Shop.ToList<Item_in_Shop>(); set { _Item_In_Shop.AddRange(value); } }

        public DbSet<Shop> _Shops { get; set; }
        public DbSet<Item> _Items { get; set; }
        public DbSet<Item_in_Shop> _Item_In_Shop { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite("Data Source=" + @"..\..\..\database.db3");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item_in_Shop>(entity => {
                entity.HasKey(k => new { k.shop_id, k.item_id });
            });
            modelBuilder.Entity<Shop>(entity =>
            {
                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasColumnType("int");
                entity.Property(e => e.name)
                    .HasColumnName("name")
                    .HasColumnType("text");
                entity.Property(e => e.address)
                    .HasColumnName("address")
                    .HasColumnType("text");
            });
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");
            });
        }
        public object add(object a)
        {
            return Add(a);
        }
        public object add(Shop a) { return Add(a); }
        public object add(Item a) { return Add(a); }
        public object add(Item_in_Shop a) { return Add(a); }
    }
    public class ShoppingCSV: IModel
    {
        
        public List<Shop> Shops { get ; set; }
        public List<Item> Items { get; set; }
        public List<Item_in_Shop> Item_in_Shop { get; set; }
        public ShoppingCSV()
        {
            using (var reader = new StreamReader(@"..\..\..\Shops.csv"))
            using (var csv = new CsvReader(reader))
            {
                Shops = csv.GetRecords<Shop>().ToList();
                Console.WriteLine(Shops[3].address);
            }
            using (var reader = new StreamReader(@"..\..\..\Items.csv"))
            using (var csv = new CsvReader(reader))
            {
                Items = csv.GetRecords<Item>().ToList();
            }
            using (var reader = new StreamReader(@"..\..\..\Item_in_Shop.csv"))
            using (var csv = new CsvReader(reader))
            {
                //csv.Configuration.Delimiter = ",";
                Item_in_Shop = csv.GetRecords<Item_in_Shop>().ToList();
            }
        }
        public object add(Shop record)
        {
            using (var writer = new StreamWriter(@"..\..\..\Shops.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(record);
            }
            return null;
        }
        public object add(Item record)
        {
            using (var writer = new StreamWriter(@"..\..\..\Items.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecord(record);
            }
            return null;
        }
        public object add(Item_in_Shop record)
        {
            using (var writer = new StreamWriter(@"..\..\..\Item_in_Shop.csv"))
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
    public class DAO: IDisposable
    {
        private string mode;
        private bool disposed;
        public DAO()
        {
            using (StreamReader file = new StreamReader(@"..\..\..\config.txt"))
            {
                string line = file.ReadLine();
                if (line == "database")
                {
                    mode = "db";
                }
                else { mode = "csv"; }
            }
        }
        public IModel data()
        {
            if (mode == "db") { return new ShoppingDB(); }
            else { return new ShoppingCSV(); }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            // Free any unmanaged objects here.
            //
            disposed = true;
        }
        ~DAO()
        {
            Dispose(false);
        }
    }
    [Table("Shops")]
    public class Shop
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }
    [Table("Items")]
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    [Table("Item_in_Shop")]
    public class Item_in_Shop
    {
        public int item_id { get; set; }
        public int shop_id { get; set; }
        public int price { get; set; }
        public int amount { get; set; }
    }
    public class Controller
    {
        DAO dao = new DAO();
        public void createShop(string n, string a)
        {
            using (var db = dao.data())
            {
                Console.WriteLine("Inserting a new shop");
                db.add(new Shop { name = n, address = a });
                try { db.SaveChanges(); } catch { };
            }
        }
        public void createItem(string n)
        {
            using (var db = dao.data())
            {
                Console.WriteLine("Inserting a new item");
                db.add(new Item { name = n });
                try { db.SaveChanges(); } catch { };
            }
        }
        public void createItemInShop(string n, int p, int a, int s)
        {
            using (var db = dao.data())
            {
                Console.WriteLine("Inserting a new item in shop");
                var it = db.Items.ToList().Find(x => x.name == n);
                if (it == null) 
                { 
                    createItem(n); 
                    it = db.Items.ToList().Find(x => x.name == n); 
                }
                Console.WriteLine(it.name);
                var fi = db.Item_in_Shop.ToList().Find(x => x.item_id == it.id && x.shop_id == s);
                if (fi == null)
                {
                    db.add(new Item_in_Shop { item_id = it.id, shop_id = s, price = p, amount = a });
                }
                else
                {
                    fi.price = p; fi.amount += a;
                }
                db.SaveChanges();
            }
        }
        public void MostCheapest(string n)
        {
            using (var db = dao.data())
            {
                Console.WriteLine("Searching shop with most cheapest {0}", n);
                var it = db.Items.ToList().Find(x => x.name == n);
                int MAX = int.MaxValue;
                int shopid = -1;
                foreach (Item_in_Shop i in db.Item_in_Shop.ToList())
                {
                    if (i.item_id == it.id && i.price < MAX) { MAX = i.price; shopid = i.shop_id; }
                }
                if (MAX < int.MaxValue && shopid != -1)
                {
                    var sh = db.Shops.ToList().Find(x => x.id == shopid);
                    Console.WriteLine("{0} \n address: {1}", sh.name, sh.address);
                }
                else { Console.WriteLine("Shop not found"); }
            }
        }
        public void MoreGoods(int money, int s)
        {
            using (var db = dao.data())
            {
                Console.WriteLine("What you can buy on {0}:", money);
                var big = db.Item_in_Shop.ToList().Find(x => x.price < money && x.shop_id == s);
                if (big != null) { Console.WriteLine(db.Items.Find(x => x.id == big.item_id).name); money -= big.price; }
                else { return; }
            }
        }
        public int Buy(string n, int sh, int num)
        {
            using (var db = dao.data())
            {
                var it = db.Items.ToList().Find(x => x.name == n);
                var good = db.Item_in_Shop.ToList().Find(x => x.item_id == it.id && x.shop_id == sh);
                if (num < good.amount) { Console.WriteLine("Total price: {0}",good.price * num); return good.price * num; }
                else { Console.WriteLine("Not enough goods"); return 0; }
            }
        }
    }
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
