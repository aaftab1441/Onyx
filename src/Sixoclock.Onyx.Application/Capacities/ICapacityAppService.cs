using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Capacities.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Capacities
{
    public interface ICapacityAppService : IApplicationService
    {
        Task CreateOrUpdateCapacity(CreateOrUpdateCapacityInput input);
        Task DeleteCapacity(EntityDto<int> input);
        GetCapacitiesListOutput GetCapacitiesList();
        Task<PagedResultDto<CapacityDto>> GetCapacity(GetCapacityInput input);
        Task<GetCapacityForEditOutput> GetCapacityForEdit(EntityDto<int> input);
    }
}