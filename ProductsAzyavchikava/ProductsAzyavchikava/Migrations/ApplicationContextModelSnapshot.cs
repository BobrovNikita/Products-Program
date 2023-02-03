﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsAzyavchikava;

#nullable disable

namespace ProductsAzyavchikava.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductsAzyavchikava.Model.CompositionRequest", b =>
                {
                    b.Property<Guid>("CompositionRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sum")
                        .HasColumnType("int");

                    b.HasKey("CompositionRequestId");

                    b.HasIndex("ProductId");

                    b.HasIndex("RequestId");

                    b.ToTable("CompositionRequests");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("Hatch")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Markup")
                        .HasColumnType("int");

                    b.Property<int>("NDS")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("Product_TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Production")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Retail_Price")
                        .HasColumnType("int");

                    b.Property<Guid>("StorageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VendorCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int>("Weight_Per_Price")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("Product_TypeId");

                    b.HasIndex("StorageId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.ProductIntoShop", b =>
                {
                    b.Property<Guid>("ProductIntoShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductIntoShopId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShopId");

                    b.ToTable("ProductIntoShops");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Product_Type", b =>
                {
                    b.Property<Guid>("Product_TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Product_TypeId");

                    b.ToTable("Product_Types");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Request", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Car")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Driver")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Number_Packages")
                        .HasColumnType("int");

                    b.Property<int>("Products_Count")
                        .HasColumnType("int");

                    b.Property<int>("Request_Cost")
                        .HasColumnType("int");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StorageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Weigh")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.HasIndex("ShopId");

                    b.HasIndex("StorageId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Shop", b =>
                {
                    b.Property<Guid>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Shop_Adress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Shop_Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Shop_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Shop_Number")
                        .HasColumnType("int");

                    b.Property<string>("Shop_Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShopId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Shop_Type", b =>
                {
                    b.Property<Guid>("Shop_TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Product_TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Shop_Count")
                        .HasColumnType("int");

                    b.HasKey("Shop_TypeId");

                    b.HasIndex("Product_TypeId");

                    b.HasIndex("ShopId");

                    b.ToTable("Shop_Types");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Storage", b =>
                {
                    b.Property<Guid>("StorageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Storage_Adress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Storage_Number")
                        .HasColumnType("int");

                    b.Property<string>("Storage_Purpose")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StorageId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.CompositionRequest", b =>
                {
                    b.HasOne("ProductsAzyavchikava.Model.Product", "Product")
                        .WithMany("CompositionRequests")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProductsAzyavchikava.Model.Request", "Request")
                        .WithMany("CompositionRequests")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Product", b =>
                {
                    b.HasOne("ProductsAzyavchikava.Model.Product_Type", "Product_Type")
                        .WithMany("Products")
                        .HasForeignKey("Product_TypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProductsAzyavchikava.Model.Storage", "Storage")
                        .WithMany("Products")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product_Type");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.ProductIntoShop", b =>
                {
                    b.HasOne("ProductsAzyavchikava.Model.Product", "Product")
                        .WithMany("ProductIntoShops")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProductsAzyavchikava.Model.Shop", "Shop")
                        .WithMany("ProductsIntoShops")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Request", b =>
                {
                    b.HasOne("ProductsAzyavchikava.Model.Shop", "Shop")
                        .WithMany("Requests")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProductsAzyavchikava.Model.Storage", "Storage")
                        .WithMany("Requests")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Shop");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Shop_Type", b =>
                {
                    b.HasOne("ProductsAzyavchikava.Model.Product_Type", "Product_Type")
                        .WithMany("Shop_Types")
                        .HasForeignKey("Product_TypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProductsAzyavchikava.Model.Shop", "Shop")
                        .WithMany("Shop_Types")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product_Type");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Product", b =>
                {
                    b.Navigation("CompositionRequests");

                    b.Navigation("ProductIntoShops");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Product_Type", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Shop_Types");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Request", b =>
                {
                    b.Navigation("CompositionRequests");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Shop", b =>
                {
                    b.Navigation("ProductsIntoShops");

                    b.Navigation("Requests");

                    b.Navigation("Shop_Types");
                });

            modelBuilder.Entity("ProductsAzyavchikava.Model.Storage", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
