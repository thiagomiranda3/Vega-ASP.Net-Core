using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Core
{
    public interface IPhotoService
    {
         Task<Photo> UploadPhotoAsync(Vehicle vehicle, IFormFile file, string uploadsFolderPath);
    }
}