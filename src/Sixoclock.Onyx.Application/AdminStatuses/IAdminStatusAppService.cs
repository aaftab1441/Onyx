using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.AdminStatuses.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.AdminStatuses
{
    public interface IAdminStatusAppService : IApplicationService
    {
        Task CreateOrUpdateAdminStatus(CreateOrUpdateAdminStatusInput input);
        Task DeleteAdminStatus(EntityDto<int> input);
        Task<PagedResultDto<AdminStatusDto>> GetAdminStatus(GetAdminStatusInput input);
        GetAdminStatusesListOutput GetAdminStatusesList();
        Task<GetAdminStatusForEditOutput> GetAdminStatusForEdit(EntityDto<int> input);
    }
}