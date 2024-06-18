﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestAPI.Data;

#nullable disable

namespace TestAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class TestDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("TestAPI.Models.Delivery", b =>
                {
                    b.Property<int>("DeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryId"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DeliveryId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ItemId");

                    b.ToTable("Deliveries");
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

            modelBuilder.Entity("TestAPI.Models.Return", b =>
                {
                    b.Property<int>("ReturnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReturnId"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReturnId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ItemId");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("TestAPI.Models.Delivery", b =>
                {
                    b.HasOne("TestAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("TestAPI.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.Navigation("Customer");

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

            modelBuilder.Entity("TestAPI.Models.Return", b =>
                {
                    b.HasOne("TestAPI.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("TestAPI.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.Navigation("Customer");

                    b.Navigation("Item");
                });
#pragma warning restore 612, 618
        }
    }
}
