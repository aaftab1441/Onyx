using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Groups.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Groups
{
    public interface IGroupAppService : IApplicationService
    {
        Task CreateOrUpdateGroup(CreateOrUpdateGroupInput input);
        Task DeleteGroup(EntityDto<int> input);
        Task<GetGroupsListOutput> GetGroupsList();
        Task<GetGroupsListOutput> GetGroupsListByInstall(EntityDto<int> input);
        Task<PagedResultDto<GroupDto>> GetGroup(GetGroupInput input);
        Task<GetGroupForEditOutput> GetGroupForEdit(EntityDto<int> input);
    }
}