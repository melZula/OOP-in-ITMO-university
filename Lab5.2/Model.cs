using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5._2
{
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

}