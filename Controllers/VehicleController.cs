using System;
using System.Collections.Generic;
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
        private readonly IVehicleRepository vehicleRepository;
        public VehicleController(IMapper mapper, VegaDbContext context, IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await vehicleRepository.GetAllAsync();
            if (vehicles == null)
                return NotFound();

            return Ok(mapper.Map<ICollection<Vehicle>, ICollection<VehicleResource>>(vehicles));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vehicle = await vehicleRepository.GetAsync(id);
            if (vehicle == null)
                return NotFound();

            return Ok(mapper.Map<Vehicle, VehicleResource>(vehicle));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificação para não causar erro 500 quando o ModelId passado não existir
            var model = await context.Models.FindAsync(saveVehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            vehicleRepository.Add(vehicle);
            await context.SaveChangesAsync();

            vehicle = await vehicleRepository.GetAsync(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await context.Models.FindAsync(saveVehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }

            var vehicle = await vehicleRepository.GetAsync(id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await vehicleRepository.GetAsync(id, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            vehicleRepository.Remove(vehicle);
            await context.SaveChangesAsync();

            return Ok(id);
        }
    }
}