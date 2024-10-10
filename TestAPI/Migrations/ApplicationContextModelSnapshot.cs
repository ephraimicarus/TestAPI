﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestAPI.Data;

#nullable disable

namespace BaseApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BaseApi.Models.TransactionModel", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TestAPI.Models.BaseInventory", b =>
                {
                    b.Property<int>("BaseInventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BaseInventoryId"));

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityRented")
                        .HasColumnType("int");

                    b.Property<int>("QuantityStored")
                        .HasColumnType("int");

                    b.HasKey("BaseInventoryId");

                    b.HasIndex("ItemId");

                    b.ToTable("BaseInventory");
                });

            modelBuilder.Entity("TestAPI.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Oib")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<bool>("Overdue")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CustomerId");

                    b.HasIndex("Oib")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TestAPI.Models.Inventory", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryId"));

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CustomerId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("TestAPI.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("TestAPI.Models.StockDelivery", b =>
                {
                    b.Property<int>("StockDeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockDeliveryId"));

                    b.Property<int?>("InventoryCustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("InventoryItemId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityDelivered")
                        .HasColumnType("int");

                    b.Property<int>("QuantityToReturn")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionInfoTransactionId")
                        .HasColumnType("int");

                    b.HasKey("StockDeliveryId");

                    b.HasIndex("TransactionInfoTransactionId");

                    b.HasIndex("InventoryCustomerId", "InventoryItemId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("TestAPI.Models.StockJournal", b =>
                {
                    b.Property<int>("StockJournalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockJournalId"));

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("StockJournalId");

                    b.ToTable("StockJournals");
                });

            modelBuilder.Entity("TestAPI.Models.StockReturn", b =>
                {
                    b.Property<int>("StockReturnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockReturnId"));

                    b.Property<int?>("DeliveryStockDeliveryId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityReturned")
                        .HasColumnType("int");

                    b.HasKey("StockReturnId");

                    b.HasIndex("DeliveryStockDeliveryId");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("TestAPI.Models.BaseInventory", b =>
                {
                    b.HasOne("TestAPI.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("TestAPI.Models.Inventory", b =>
                {
                    b.HasOne("TestAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestAPI.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("TestAPI.Models.StockDelivery", b =>
                {
                    b.HasOne("BaseApi.Models.TransactionModel", "TransactionInfo")
                        .WithMany()
                        .HasForeignKey("TransactionInfoTransactionId");

                    b.HasOne("TestAPI.Models.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryCustomerId", "InventoryItemId");

                    b.Navigation("Inventory");

                    b.Navigation("TransactionInfo");
                });

            modelBuilder.Entity("TestAPI.Models.StockReturn", b =>
                {
                    b.HasOne("TestAPI.Models.StockDelivery", "Delivery")
                        .WithMany()
                        .HasForeignKey("DeliveryStockDeliveryId");

                    b.Navigation("Delivery");
                });
#pragma warning restore 612, 618
        }
    }
}
