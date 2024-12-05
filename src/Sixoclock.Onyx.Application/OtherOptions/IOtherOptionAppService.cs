using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.OtherOptions.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.OtherOptions
{
    public interface IOtherOptionAppService : IApplicationService
    {
        Task CreateOrUpdateOtherOption(CreateOrUpdateOtherOptionInput input);
        Task DeleteOtherOption(EntityDto<int> input);
        Task<PagedResultDto<OtherOptionDto>> GetOtherOption(GetOtherOptionInput input);
        Task<GetOtherOptionForEditOutput> GetOtherOptionForEdit(EntityDto<int> input);
    }
}