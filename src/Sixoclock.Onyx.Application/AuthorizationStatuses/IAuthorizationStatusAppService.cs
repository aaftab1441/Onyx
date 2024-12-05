using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.AuthorizationStatuses.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.AuthorizationStatuses
{
    public interface IAuthorizationStatusAppService : IApplicationService
    {
        Task CreateOrUpdateAuthorizationStatus(CreateOrUpdateAuthorizationStatusInput input);
        Task DeleteAuthorizationStatus(EntityDto<int> input);
        ListResultDto<AuthorizationStatusDto> GetAuthorizationStatus(GetAuthorizationStatusInput input);
        Task<GetAuthorizationStatusForEditOutput> GetAuthorizationStatusForEdit(EntityDto<int> input);
        GetAuthorizationStatussListOutput GetAuthorizationStatusesList();
    }
}