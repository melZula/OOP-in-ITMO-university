using System;
using System.Collections.Generic;

namespace Lab5._2
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
}