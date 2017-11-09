using System.Collections.Generic;
using System.Threading.Tasks;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Core
{
    public interface IMakeRepository
    {
        Task<ICollection<Make>> GetAllAsync(bool includeRelated = true);
        Task<Make> GetAsync(int id, bool includeRelated = true);
        void Add(Make make);
        void Remove(Make make);
    }
}