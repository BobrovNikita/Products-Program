using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductsAzyavchikava.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductsAzyavchikava
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CompositionRequest> CompositionRequests { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Type> Product_Types { get; set; }
        public DbSet<ProductIntoShop> ProductIntoShops { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Shop_Type> Shop_Types { get; set; }
        public DbSet<Storage> Storages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ProductAzyavch;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CompositionRequest
            modelBuilder
                .Entity<CompositionRequest>()
                .HasOne(e => e.Request)
                .WithMany(e => e.CompositionRequests)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<CompositionRequest>()
                .HasOne(e => e.Product)
                .WithMany(e => e.CompositionRequests)
                .OnDelete(DeleteBehavior.NoAction);

            //Product
            modelBuilder
                .Entity<Product>()
                .HasOne(e => e.Storage)
                .WithMany(e => e.Products)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Product>()
                .HasOne(e => e.Product_Type)
                .WithMany(e => e.Products)
                .OnDelete(DeleteBehavior.NoAction);

            //Request

            modelBuilder
                .Entity<Request>()
                .HasOne(e => e.Storage)
                .WithMany(e => e.Requests)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Request>()
                .HasOne(e => e.Shop)
                .WithMany(e => e.Requests)
                .OnDelete(DeleteBehavior.NoAction);

            //Shop Type

            modelBuilder
                .Entity<Shop_Type>()
                .HasOne(e => e.Shop)
                .WithMany(e => e.Shop_Types)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Shop_Type>()
                .HasOne(e => e.Product_Type)
                .WithMany(e => e.Shop_Types)
                .OnDelete(DeleteBehavior.NoAction);

            //ProductIntoShop

            modelBuilder
                .Entity<ProductIntoShop>()
                .HasOne(e => e.Product)
                .WithMany(e => e.ProductIntoShops)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<ProductIntoShop>()
                .HasOne(e => e.Shop)
                .WithMany(e => e.ProductsIntoShops)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
