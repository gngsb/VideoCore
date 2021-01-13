using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoManage.Service.Extends
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<T> PageByIndex<T>(this IQueryable<T> query, int pageIndex, int pageSize) 
        {
            if (query == null) 
            {
                throw new Exception("查询数据为空");
            }
            if (pageIndex <= 0) 
            {
                pageIndex = 1;
            }
            if (pageSize <= 0) 
            {
                pageSize = 10;
            }
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
