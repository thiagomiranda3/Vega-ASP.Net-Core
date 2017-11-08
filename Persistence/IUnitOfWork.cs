using System.Threading.Tasks;

namespace Vega_ASP.Net_Core.Persistence
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}