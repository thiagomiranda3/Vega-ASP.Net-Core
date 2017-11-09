using System.Threading.Tasks;

namespace Vega_ASP.Net_Core.Core
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}