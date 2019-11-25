using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Item_in_Shop> Item_In_Shop { get; set; }

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
    }

    public class Shop
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class Item_in_Shop
    {
        public int item_id { get; set; }
        public int shop_id { get; set; }
        public int price { get; set; }
        public int amount { get; set; }
    }
    public class Controller
    {
        public void createShop(string n, string a)
        {
            using (var db = new ShoppingContext())
            {
                Console.WriteLine("Inserting a new shop");
                db.Add(new Shop { name = n, address = a });
                try { db.SaveChanges(); } catch { };
            }
        }
        public void createItem(string n)
        {
            using (var db = new ShoppingContext())
            {
                Console.WriteLine("Inserting a new item");
                db.Add(new Item { name = n });
                try { db.SaveChanges(); } catch { };
            }
        }
        public void createItemInShop(string n, int p, int a, int s)
        {
            using (var db = new ShoppingContext())
            {
                Console.WriteLine("Inserting a new item in shop");
                var it = db.Items.ToList().Find(x => x.name == n);
                if (it == null) 
                { 
                    createItem(n); 
                    it = db.Items.ToList().Find(x => x.name == n); 
                }
                Console.WriteLine(it.name);
                var fi = db.Item_In_Shop.ToList().Find(x => x.item_id == it.id && x.shop_id == s);
                if (fi == null)
                {
                    db.Add(new Item_in_Shop { item_id = it.id, shop_id = s, price = p, amount = a });
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
            using (var db = new ShoppingContext())
            {
                Console.WriteLine("Searching shop with most cheapest {0}", n);
                var it = db.Items.ToList().Find(x => x.name == n);
                int MAX = int.MaxValue;
                int shopid = -1;
                foreach (Item_in_Shop i in db.Item_In_Shop.ToList())
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
            using (var db = new ShoppingContext())
            {
                Console.WriteLine("What you can buy on {0}:", money);
                var big = db.Item_In_Shop.ToList().Find(x => x.price < money && x.shop_id == s);
                if (big != null) { Console.WriteLine(db.Items.Find(big.item_id).name); money -= big.price; }
                else { return; }
            }
        }
        public int Buy(string n, int sh, int num)
        {
            using (var db = new ShoppingContext())
            {
                var it = db.Items.ToList().Find(x => x.name == n);
                var good = db.Item_In_Shop.ToList().Find(x => x.item_id == it.id && x.shop_id == sh);
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
            //cnt.createItemInShop("Шоколад", 39, 7, 1);
            //cnt.MostCheapest("Шоколад");
            cnt.MoreGoods(100, 4);
            Console.ReadKey();
        }
    }
}
