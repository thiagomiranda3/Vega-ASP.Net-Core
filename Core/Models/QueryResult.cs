using System.Collections.Generic;

namespace Vega_ASP.Net_Core.Core.Models
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}