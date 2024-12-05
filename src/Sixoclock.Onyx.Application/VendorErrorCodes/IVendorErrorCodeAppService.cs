using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.VendorErrorCodes.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.VendorErrorCodes
{
    public interface IVendorErrorCodeAppService : IApplicationService
    {
        Task CreateOrUpdateVendorErrorCode(CreateOrUpdateVendorErrorCodeInput input);
        Task DeleteVendorErrorCode(EntityDto<int> input);
        Task<PagedResultDto<VendorErrorCodeDto>> GetVendorErrorCode(GetVendorErrorCodeInput input);
        Task<GetVendorErrorCodeForEditOutput> GetVendorErrorCodeForEdit(EntityDto<int> input);
    }
}