using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vega_ASP.Net_Core.Controllers.Resources;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.vehicleRepository = vehicleRepository;
            this.host = host;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.GetAsync(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            // O GUID previne ataques do cliente mudando na requisição o caminho do arquivo
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}