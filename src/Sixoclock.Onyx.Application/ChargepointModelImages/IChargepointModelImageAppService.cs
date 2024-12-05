using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ChargepointModelImages.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ChargepointModelImages
{
    public interface IChargepointModelImageAppService : IApplicationService
    {
        Task CreateOrUpdateChargepointModelImage(CreateOrUpdateChargepointModelImageInput input);
        Task DeleteChargepointModelImage(EntityDto<int> input);
        Task<PagedResultDto<ChargepointModelImageDto>> GetChargepointModelImage(GetChargepointModelImageInput input);
        Task<GetChargepointModelImageForEditOutput> GetChargepointModelImageForEdit(EntityDto<int> input);
    }
}