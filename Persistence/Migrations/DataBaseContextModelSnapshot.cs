﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CatalogItemDiscount", b =>
                {
                    b.Property<int>("CatalogItemsId")
                        .HasColumnType("int");

                    b.Property<int>("DiscountsId")
                        .HasColumnType("int");

                    b.HasKey("CatalogItemsId", "DiscountsId");

                    b.HasIndex("DiscountsId");

                    b.ToTable("CatalogItemDiscount");
                });

            modelBuilder.Entity("Domain.Baskets.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 56, DateTimeKind.Local).AddTicks(9600));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Domain.Baskets.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 57, DateTimeKind.Local).AddTicks(1540));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("CatalogItemId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 57, DateTimeKind.Local).AddTicks(3719));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CatalogBrand", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Logitech"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Apple"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Acer"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "SAMSUNG"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Lenovo"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "ASUS"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "HP"
                        },
                        new
                        {
                            Id = 8,
                            Brand = "MSI"
                        },
                        new
                        {
                            Id = 9,
                            Brand = "Western Digital"
                        },
                        new
                        {
                            Id = 10,
                            Brand = "Canon"
                        });
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AvailableStock")
                        .HasColumnType("int");

                    b.Property<int>("CatalogBrandId")
                        .HasColumnType("int");

                    b.Property<int>("CatalogTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 57, DateTimeKind.Local).AddTicks(6043));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("MaxStockThreshold")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestockThreshold")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CatalogBrandId");

                    b.HasIndex("CatalogTypeId");

                    b.ToTable("CatalogItems");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogItemFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("int");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 57, DateTimeKind.Local).AddTicks(9442));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId");

                    b.ToTable("CatalogItemFeature");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 58, DateTimeKind.Local).AddTicks(1435));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Src")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId");

                    b.ToTable("CatalogItemImage");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 58, DateTimeKind.Local).AddTicks(3448));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("ParentCatalogTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentCatalogTypeId");

                    b.ToTable("CatalogType", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            ParentCatalogTypeId = 1,
                            Type = "Accessories & Supplies"
                        },
                        new
                        {
                            Id = 3,
                            ParentCatalogTypeId = 1,
                            Type = "Camera & Photo"
                        },
                        new
                        {
                            Id = 4,
                            ParentCatalogTypeId = 1,
                            Type = "Car & Vehicle Electronics"
                        },
                        new
                        {
                            Id = 5,
                            ParentCatalogTypeId = 1,
                            Type = "Cell Phones & Accessories"
                        },
                        new
                        {
                            Id = 6,
                            ParentCatalogTypeId = 1,
                            Type = "Computers & Accessories"
                        },
                        new
                        {
                            Id = 7,
                            Type = "Computers"
                        },
                        new
                        {
                            Id = 8,
                            ParentCatalogTypeId = 7,
                            Type = "Computer Accessories & Peripherals"
                        },
                        new
                        {
                            Id = 9,
                            ParentCatalogTypeId = 7,
                            Type = "Computer Components"
                        },
                        new
                        {
                            Id = 10,
                            ParentCatalogTypeId = 7,
                            Type = "Computers & Tablets"
                        },
                        new
                        {
                            Id = 11,
                            ParentCatalogTypeId = 7,
                            Type = "Data Storage"
                        });
                });

            modelBuilder.Entity("Domain.Discounts.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("CouponCode")
                        .HasColumnType("bit");

                    b.Property<int>("DiscountAmount")
                        .HasColumnType("int");

                    b.Property<int>("DiscountLimitationId")
                        .HasColumnType("int");

                    b.Property<int>("DiscountPercentage")
                        .HasColumnType("int");

                    b.Property<int>("DiscountTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 58, DateTimeKind.Local).AddTicks(5872));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("RequiresCouponCode")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("UsePercentage")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Domain.Order.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 58, DateTimeKind.Local).AddTicks(9963));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Order.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 59, DateTimeKind.Local).AddTicks(4665));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("PictureUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Payments.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Authority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DatePay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 59, DateTimeKind.Local).AddTicks(8895));

                    b.Property<bool>("IsPay")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<long>("RefId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Users.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 14, 16, 13, 1, 60, DateTimeKind.Local).AddTicks(1713));

                    b.Property<bool>("IsRemove")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("PostalAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReciverName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("CatalogItemDiscount", b =>
                {
                    b.HasOne("Domain.Catalogs.CatalogItem", null)
                        .WithMany()
                        .HasForeignKey("CatalogItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Discounts.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Baskets.BasketItem", b =>
                {
                    b.HasOne("Domain.Baskets.Basket", null)
                        .WithMany("Items")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Catalogs.CatalogItem", "CatalogItem")
                        .WithMany()
                        .HasForeignKey("CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogItem");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogItem", b =>
                {
                    b.HasOne("Domain.Catalogs.CatalogBrand", "CatalogBrand")
                        .WithMany()
                        .HasForeignKey("CatalogBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Catalogs.CatalogType", "CatalogType")
                        .WithMany()
                        .HasForeignKey("CatalogTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogBrand");

                    b.Navigation("CatalogType");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogItemFeature", b =>
                {
                    b.HasOne("Domain.Catalogs.CatalogItem", "CatalogItem")
                        .WithMany("CatalogItemFeatures")
                        .HasForeignKey("CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogItem");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogItemImage", b =>
                {
                    b.HasOne("Domain.Catalogs.CatalogItem", "CatalogItem")
                        .WithMany("CatalogItemImages")
                        .HasForeignKey("CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogItem");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogType", b =>
                {
                    b.HasOne("Domain.Catalogs.CatalogType", "ParentCatalogType")
                        .WithMany("SubType")
                        .HasForeignKey("ParentCatalogTypeId");

                    b.Navigation("ParentCatalogType");
                });

            modelBuilder.Entity("Domain.Order.Order", b =>
                {
                    b.OwnsOne("Domain.Order.Address", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostalAddress")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ReciverName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Order.OrderItem", b =>
                {
                    b.HasOne("Domain.Order.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Domain.Payments.Payment", b =>
                {
                    b.HasOne("Domain.Order.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Baskets.Basket", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogItem", b =>
                {
                    b.Navigation("CatalogItemFeatures");

                    b.Navigation("CatalogItemImages");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogType", b =>
                {
                    b.Navigation("SubType");
                });

            modelBuilder.Entity("Domain.Order.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
