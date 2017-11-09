using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Persistence
{
    public class ModelRepository : IModelRepository
    {
        private readonly VegaDbContext context;
        
        public ModelRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Model>> GetAllAsync(bool includeRelated)
        {
            if(!includeRelated)
                return await context.Models.ToListAsync();
            
            return await context.Models
                                .Include(m => m.Make)
                                .ToListAsync();
        }

        public async Task<Model> GetAsync(int id, bool includeRelated)
        {
            if(!includeRelated)
                return await context.Models.SingleOrDefaultAsync(m => m.Id == id);

            return await context.Models
                                .Include(m => m.Make)
                                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public void Add(Model model)
        {
            context.Models.Add(model);
        }

        public void Remove(Model model)
        {
            context.Models.Remove(model);
        }
    }
}