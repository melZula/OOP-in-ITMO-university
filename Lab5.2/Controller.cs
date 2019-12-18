using System;
using System.Linq;
using System.IO;

namespace Lab5._2
{
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

}