using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;
using Vega_ASP.Net_Core.Extensions;

namespace Vega_ASP.Net_Core.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Vehicle>> GetAllAsync(VehicleQuery queryObj, bool includeRelated = true)
        {
            var query = context.Vehicles.AsQueryable();
            
            if(includeRelated)
                query = query.Include(v => v.Features)
                             .ThenInclude(vf => vf.Feature)
                             .Include(v => v.Model)
                             .ThenInclude(m => m.Make)
                             .AsQueryable();
            
            if(queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId);

            if(queryObj.MakeId.HasValue)
                query = query.Where(v => v.ModelId == queryObj.ModelId);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };

            query = query.ApplyOrdering(queryObj, columnsMap);
            query = query.ApplyPaging(queryObj);

            return await query.ToListAsync();
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