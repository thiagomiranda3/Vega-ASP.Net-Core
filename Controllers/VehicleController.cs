using Microsoft.AspNetCore.Mvc;
using Vega_ASP.Net_Core.Models;

namespace Vega_ASP.Net_Core.Controllers
{
    [Route("/api/vehicle")]
    public class VehicleController : Controller
    {
        [HttpPost]
        public IActionResult Post(Vehicle vehicle){
            return Ok(vehicle);
        }
    }
}