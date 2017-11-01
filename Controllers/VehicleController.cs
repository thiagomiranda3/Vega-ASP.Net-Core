using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Controllers.Resources;
using Vega_ASP.Net_Core.Models;
using Vega_ASP.Net_Core.Persistence;

namespace Vega_ASP.Net_Core.Controllers
{
    [Route("/api/vehicle")]
    public class VehicleController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;
        public VehicleController(IMapper mapper, VegaDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificação para não causar erro 500 quando o ModelId passado não existir
            var model = await context.Models.FindAsync(vehicleResource.ModelId);
            if(model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await context.Models.FindAsync(vehicleResource.ModelId);
            if(model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }

            var vehicle = await context.Vehicles
                                       .Include(v => v.Features)
                                       .SingleOrDefaultAsync(v => v.Id == id);
            mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
    }
}