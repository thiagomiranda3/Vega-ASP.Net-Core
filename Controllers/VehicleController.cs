using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Controllers.Resources;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;
using Vega_ASP.Net_Core.Persistence;

namespace Vega_ASP.Net_Core.Controllers
{
    [Route("/api/vehicles")]
    public class VehicleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IModelRepository modelRepository;
        public VehicleController(IMapper mapper, IVehicleRepository vehicleRepository, IModelRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.vehicleRepository = vehicleRepository;
            this.modelRepository = modelRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(VehicleQueryResource filterResource)
        {
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var vehicles = await vehicleRepository.GetAllAsync(filter);
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
            var model = await modelRepository.GetAsync(saveVehicleResource.ModelId, includeRelated: false);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            vehicleRepository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await vehicleRepository.GetAsync(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await modelRepository.GetAsync(saveVehicleResource.ModelId, includeRelated: false);
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

            await unitOfWork.CompleteAsync();

            vehicle = await vehicleRepository.GetAsync(id);

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
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}