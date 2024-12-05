using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.MeterTypes.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.MeterTypes
{
    public interface IMeterTypeAppService : IApplicationService
    {
        Task CreateOrUpdateMeterType(CreateOrUpdateMeterTypeInput input);
        Task DeleteMeterType(EntityDto<int> input);
        Task<PagedResultDto<MeterTypeDto>> GetMeterType(GetMeterTypeInput input);
        Task<GetMeterTypeForEditOutput> GetMeterTypeForEdit(EntityDto<int> input);
        GetMeterTypesListOutput GetMeterTypesList();
    }
}