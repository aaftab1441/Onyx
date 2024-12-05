using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.ChargeReleaseOptions.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.ChargeReleaseOptions
{
    public interface IChargeReleaseOptionAppService : IApplicationService
    {
        Task CreateOrUpdateChargeReleaseOption(CreateOrUpdateChargeReleaseOptionInput input);
        Task DeleteChargeReleaseOption(EntityDto<int> input);
        Task<PagedResultDto<ChargeReleaseOptionDto>> GetChargeReleaseOption(GetChargeReleaseOptionInput input);
        Task<GetChargeReleaseOptionForEditOutput> GetChargeReleaseOptionForEdit(EntityDto<int> input);
    }
}