using System.Linq.Expressions;

namespace Common
{
    public static class PagingExtensions
    {
        // used by LINQ to SQL
        public static IQueryable<TSource> ToPaged<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page-1)*pageSize).Take(pageSize);
        }

        //--------used by LINQ--------
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowCount)
        {
            rowCount = source.Count();
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query">Your query</param>
        /// <param name="pageNum">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="orderByPreperty">Items order</param>
        /// <param name="isAscendingOrder">if true returns ASC </param>
        /// <param name="rowsCount">Count of rows</param>
        /// <returns></returns>
        public static IQueryable<T> PagedResult<T, TResult>(this IQueryable<T> query, int pageNum, int pageSize,
            Expression<Func<T, TResult>> orderByPreperty, bool isAscendingOrder, out int rowsCount)
        {
           if(pageSize<=0) pageSize=20;
            rowsCount = query.Count();
            if (pageNum <= 0) pageNum = 1;
            int excludedRows = (pageNum-1)*pageSize;
            return query.Skip(excludedRows).Take(pageSize);
        }

        public static IQueryable<TSource> PagedResult<TSource>(this IQueryable<TSource> query, int pageNum, int pageSize, out int rowsCount)
        {
            if(pageSize<=0) pageSize = 20;
            rowsCount=query.Count();
            if(pageNum<=0) pageNum = 1;
            int excludedRows = (pageNum-1)*pageSize;
            return query.Skip(excludedRows).Take(pageSize);
        }

    }
}