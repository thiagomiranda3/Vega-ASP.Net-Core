using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Vehicle> ApplyFiltering(this IQueryable<Vehicle> query, VehicleQuery queryObj)
        {
            if(queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId);

            if(queryObj.MakeId.HasValue)
                query = query.Where(v => v.ModelId == queryObj.ModelId);
            
            return query;
        }
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if(query == null ||
               string.IsNullOrWhiteSpace(queryObj.SortBy) ||
               !columnsMap.ContainsKey(queryObj.SortBy))
                return query;

            if(queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            
            return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if(queryObj.Page <= 0)
                queryObj.Page = 1;
                
            if(queryObj.PageSize <= 0)
                queryObj.PageSize = 10;
                
            return query.Skip((queryObj.Page - 1) * queryObj.PageSize)
                        .Take(queryObj.PageSize);
        }
    }
}