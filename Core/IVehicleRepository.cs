using System.Collections.Generic;
using System.Threading.Tasks;
using Vega_ASP.Net_Core.Core.Models;

namespace Vega_ASP.Net_Core.Core
{
    public interface IVehicleRepository
    {
        Task<QueryResult<Vehicle>> GetAllAsync(VehicleQuery filter, bool includeRelated = true);
        Task<Vehicle> GetAsync(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}