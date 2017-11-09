using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Persistence
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly VegaDbContext context;
        public FeatureRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public void Add(Feature feature)
        {
            context.Add(feature);
        }

        public async Task<ICollection<Feature>> GetAllAsync(bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Features.ToListAsync();

            return await context.Features
                                .Include(m => m.Vehicles)
                                .ToListAsync();
        }

        public async Task<Feature> GetAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Features.SingleOrDefaultAsync(m => m.Id == id);

            return await context.Features
                                .Include(m => m.Vehicles)
                                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Feature feature)
        {
            context.Features.Remove(feature);
        }
    }
}