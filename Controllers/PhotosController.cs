using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly IMapper mapper;
        private readonly PhotoSettings options;
        private readonly IPhotoRepository photoRepository;
        private readonly IPhotoService photoService;

        public PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IPhotoRepository photoRepository, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoService photoService)
        {
            this.photoService = photoService;
            this.photoRepository = photoRepository;
            this.options = options.Value;
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
            this.host = host;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int vehicleId)
        {
            var vehicle = await vehicleRepository.GetAsync(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            var photos = await photoRepository.GetPhotosAsync(vehicleId);
            return Ok(mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos));
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.GetAsync(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            if (file == null)
                return BadRequest("File cannot be null");

            if (file.Length == 0)
                return BadRequest("File cannot be empty");

            if (file.Length > options.MaxBytes)
                return BadRequest("File is bigger than 10 MB");

            if (!options.IsSupported(file.FileName))
                return BadRequest("Invalid file type!");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            
            var photo = await photoService.UploadPhotoAsync(vehicle, file, uploadsFolderPath);

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}