using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public class DatabaseContextSeed
    {
        public static void CatalogSeed(ModelBuilder modelBuilder)
        {
            foreach (var catalog in GetCatalogTypes())
            {
                modelBuilder.Entity<CatalogType>().HasData(catalog);
            }

            foreach (var catalog in GetCatalogBrands())
            {
                modelBuilder.Entity<CatalogBrand>().HasData(catalog);
            }
        }
        private static IEnumerable<CatalogType> GetCatalogTypes()
        {
            return new List<CatalogType>()
            {
            new CatalogType() {Id = 1, Type ="Electronics"},
            new CatalogType() {Id = 2, Type ="Accessories & Supplies", ParentCatalogTypeId = 1},
            new CatalogType() {Id = 3, Type ="Camera & Photo", ParentCatalogTypeId = 1},
            new CatalogType() {Id = 4, Type ="Car & Vehicle Electronics", ParentCatalogTypeId = 1},
            new CatalogType() {Id = 5, Type ="Cell Phones & Accessories", ParentCatalogTypeId = 1},
            new CatalogType() {Id = 6, Type ="Computers & Accessories", ParentCatalogTypeId = 1},
            new CatalogType() {Id = 7, Type ="Computers"},
            new CatalogType() {Id = 8, Type ="Computer Accessories & Peripherals", ParentCatalogTypeId = 7},
            new CatalogType() {Id = 9, Type ="Computer Components", ParentCatalogTypeId = 7},
            new CatalogType() {Id = 10, Type ="Computers & Tablets", ParentCatalogTypeId = 7},
            new CatalogType() {Id = 11, Type ="Data Storage", ParentCatalogTypeId = 7},

            };
        }

        private static IEnumerable<CatalogBrand> GetCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
               new CatalogBrand() {Id = 1, Brand ="Logitech"},
               new CatalogBrand() {Id = 2, Brand ="Apple"},
               new CatalogBrand() {Id = 3, Brand ="Acer"},
               new CatalogBrand() {Id = 4, Brand ="SAMSUNG"},
               new CatalogBrand() {Id = 5, Brand ="Lenovo"},
               new CatalogBrand() {Id = 6, Brand ="ASUS"},
               new CatalogBrand() {Id = 7, Brand ="HP"},
               new CatalogBrand() {Id = 8, Brand ="MSI"},
               new CatalogBrand() {Id = 9, Brand ="Western Digital"},
               new CatalogBrand() {Id = 10, Brand ="Canon"},
          
            };
        }

    }

}
