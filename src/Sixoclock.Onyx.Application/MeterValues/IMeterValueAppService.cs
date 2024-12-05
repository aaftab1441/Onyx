using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.MeterValues.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.MeterValues
{
    public interface IMeterValueAppService : IApplicationService
    {
        Task CreateOrUpdateMeterValue(CreateOrUpdateMeterValueInput input);
        Task DeleteMeterValue(EntityDto<int> input);
        ListResultDto<MeterValueDto> GetMeterValueByTransaction(EntityDto<int> input);
        Task<GetMeterValueForEditOutput> GetMeterValueForEdit(EntityDto<int> input);
        GetMeterValuesListOutput GetMeterValuesList();
    }
}