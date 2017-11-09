using System.Threading.Tasks;
using Vega_ASP.Net_Core.Persistence;

namespace Vega_ASP.Net_Core.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext context;
        
        public UnitOfWork(VegaDbContext context)
        {
            this.context = context; 
        }

        public async Task CompleteAsync() => await context.SaveChangesAsync();
    }
}