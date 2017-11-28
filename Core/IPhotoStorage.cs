using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vega_ASP.Net_Core.Core
{
    public interface IPhotoStorage
    {
         Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
    }
}