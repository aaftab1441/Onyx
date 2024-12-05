using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ElectricalOptions.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ElectricalOptions
{
    public interface IElectricalOptionAppService : IApplicationService
    {
        Task CreateOrUpdateElectricalOption(CreateOrUpdateElectricalOptionInput input);
        Task DeleteElectricalOption(EntityDto<int> input);
        Task<PagedResultDto<ElectricalOptionDto>> GetElectricalOption(GetElectricalOptionInput input);
        Task<GetElectricalOptionForEditOutput> GetElectricalOptionForEdit(EntityDto<int> input);
    }
}