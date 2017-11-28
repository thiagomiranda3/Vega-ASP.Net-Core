using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vega_ASP.Net_Core.Core;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;

        public PhotoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Photo> UploadPhotoAsync(Vehicle vehicle, IFormFile file, string uploadsFolderPath)
        {
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

            return photo;
        }
    }
}