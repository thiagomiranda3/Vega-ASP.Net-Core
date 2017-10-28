using Microsoft.EntityFrameworkCore;
using Vega_ASP.Net_Core.Models;

namespace Vega_ASP.Net_Core.Persistence
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {
        }
    }
}