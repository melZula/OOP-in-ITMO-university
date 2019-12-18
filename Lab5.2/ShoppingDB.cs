using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Lab5._2
{
        public class ShoppingDB : DbContext, IModel
    {
        public List<Shop> Shops { get => _Shops.ToList<Shop>(); set { _Shops.AddRange(value); } }
        public List<Item> Items { get => _Items.ToList<Item>(); set { _Items.AddRange(value); } }
        public List<Item_in_Shop> Item_in_Shop { get => _Item_In_Shop.ToList<Item_in_Shop>(); set { _Item_In_Shop.AddRange(value); } }

        public DbSet<Shop> _Shops { get; set; }
        public DbSet<Item> _Items { get; set; }
        public DbSet<Item_in_Shop> _Item_In_Shop { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite("Data Source=" + @"database.db3");
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
}