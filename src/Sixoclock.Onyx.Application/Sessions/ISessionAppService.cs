using System.Threading.Tasks;
using Abp.Application.Services;
using Sixoclock.Onyx.Sessions.Dto;

namespace Sixoclock.Onyx.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
