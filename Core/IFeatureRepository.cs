using System.Collections.Generic;
using System.Threading.Tasks;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Core
{
    public interface IFeatureRepository
    {
        Task<ICollection<Feature>> GetAllAsync(bool includeRelated = true);
        Task<Feature> GetAsync(int id, bool includeRelated = true);
        void Add(Feature feature);
        void Remove(Feature feature);

    }
}