using System.Collections.Generic;
using System.Threading.Tasks;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Core
{
    public interface IModelRepository
    {
        Task<ICollection<Model>> GetAllAsync(bool includeRelated = true);
        Task<Model> GetAsync(int id, bool includeRelated = true);
        void Add(Model model);
        void Remove(Model model);
    }
}