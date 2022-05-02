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
                        .HasDefaultValue(new DateTime(2022, 5, 2, 1, 5, 44, 206, DateTimeKind.Local).AddTicks(4063));

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

            modelBuilder.Entity("Domain.Catalogs.CatalogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 5, 2, 1, 5, 44, 206, DateTimeKind.Local).AddTicks(5560));

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

            modelBuilder.Entity("Domain.Catalogs.CatalogType", b =>
                {
                    b.HasOne("Domain.Catalogs.CatalogType", "ParentCatalogType")
                        .WithMany("SubType")
                        .HasForeignKey("ParentCatalogTypeId");

                    b.Navigation("ParentCatalogType");
                });

            modelBuilder.Entity("Domain.Catalogs.CatalogType", b =>
                {
                    b.Navigation("SubType");
                });
#pragma warning restore 612, 618
        }
    }
}
