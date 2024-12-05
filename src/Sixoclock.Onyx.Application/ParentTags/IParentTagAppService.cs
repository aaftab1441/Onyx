using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ParentTags.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ParentTags
{
    public interface IParentTagAppService : IApplicationService
    {
        Task CreateOrUpdateParentTag(CreateOrUpdateParentTagInput input);
        Task<int> CreateOrUpdateParentTagAndGetId(CreateOrUpdateParentTagInput input);
        Task DeleteParentTag(EntityDto<int> input);
        Task<PagedResultDto<ParentTagDto>> GetParentTag(GetParentTagInput input);
        Task<GetParentTagForEditOutput> GetParentTagForEdit(EntityDto<int> input);
        GetParentTagsListOutput GetParentTagsList();
    }
}