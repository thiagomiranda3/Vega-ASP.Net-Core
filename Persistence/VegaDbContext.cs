using Microsoft.EntityFrameworkCore;

namespace Vega_ASP.Net_Core.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {
            
        }
    }
}