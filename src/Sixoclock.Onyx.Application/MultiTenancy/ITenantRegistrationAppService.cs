using System.Threading.Tasks;
using Abp.Application.Services;
using Sixoclock.Onyx.Editions.Dto;
using Sixoclock.Onyx.MultiTenancy.Dto;

namespace Sixoclock.Onyx.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}