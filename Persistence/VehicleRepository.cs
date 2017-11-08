using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Models;

namespace Vega_ASP.Net_Core.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Vehicle>> GetAllAsync(bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Vehicles.ToListAsync();

            return await context.Vehicles
                                .Include(v => v.Features)
                                .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                .ThenInclude(m => m.Make)
                                .ToListAsync();
        }

        public async Task<Vehicle> GetAsync(int id, bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Vehicles.SingleOrDefaultAsync(v => v.Id == id);

            return await context.Vehicles
                                .Include(v => v.Features)
                                .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                .ThenInclude(m => m.Make)
                                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}