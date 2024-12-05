using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Configs.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Configs
{
    public interface IConfigAppService : IApplicationService
    {
        Task CreateOrUpdateConfig(CreateOrUpdateConfigInput input);
        Task DeleteConfig(EntityDto<int> input);
        Task<PagedResultDto<ConfigDto>> GetConfig(GetConfigInput input);
        Task<GetConfigForEditOutput> GetConfigForEdit(EntityDto<int> input);
    }
}