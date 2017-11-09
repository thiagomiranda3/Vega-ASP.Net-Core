using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Persistence
{
    public class MakeRepository : IMakeRepository
    {
        private readonly VegaDbContext context;
        public MakeRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<Make>> GetAllAsync(bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Makes.ToListAsync();

            return await context.Makes
                                .Include(m => m.Models)
                                .ToListAsync();
        }

        public async Task<Make> GetAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Makes.SingleOrDefaultAsync(m => m.Id == id);

            return await context.Makes
                                .Include(m => m.Models)
                                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public void Add(Make make)
        {
            context.Makes.Add(make);
        }

        public void Remove(Make make)
        {
            context.Makes.Remove(make);
        }
    }
}