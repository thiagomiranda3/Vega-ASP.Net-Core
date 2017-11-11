using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if(queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            
            return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }
    }
}