using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Regions.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Regions
{
    public interface IRegionAppService : IApplicationService
    {
        Task CreateOrUpdateRegion(CreateOrUpdateRegionInput input);
        Task DeleteRegion(EntityDto<int> input);
        Task<PagedResultDto<RegionDto>> GetRegion(GetRegionInput input);
        Task<GetRegionForEditOutput> GetRegionForEdit(EntityDto<int> input);
        Task<GetRegionsListOutput> GetRegionsList();
        Task<GetRegionsListOutput> GetRegionsListByMarket(EntityDto<int> input);
    }
}