using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CacheHelpers
{
    public static class CacheHelper
    {
        public static TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(60);
        public static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}";
        public static string GenerateHomePageCacheKey()
        {
            return "HomePage";
        }

        public static string GenerateCatalogItemCacheKey(int pageIndex, int itemsPage, int? typeId)
        {
            return string.Format(_itemsKeyTemplate, pageIndex, itemsPage, typeId); 
        }
    }
}
