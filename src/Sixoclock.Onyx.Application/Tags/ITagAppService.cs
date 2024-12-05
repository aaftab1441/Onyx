using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Tags.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Tags
{
    public interface ITagAppService : IApplicationService
    {
        Task CreateOrUpdateTag(CreateOrUpdateTagInput input);
        Task DeleteTag(EntityDto<int> input);
        Task<PagedResultDto<TagDto>> GetTag(GetTagInput input);
        Task<GetTagForEditOutput> GetTagForEdit(EntityDto<int> input);
        TagDto GetTagForViewDetails(EntityDto<int> input);
    }
}