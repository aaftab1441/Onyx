using System.Threading.Tasks;
using Sixoclock.Onyx.Sessions.Dto;

namespace Sixoclock.Onyx.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
