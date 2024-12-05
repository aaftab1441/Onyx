using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Vendors.Dto;
using Abp.Application.Services;

namespace Sixoclock.Onyx.Vendors
{
    public interface IVendorAppService : IApplicationService
    {
        Task CreateOrUpdateVendor(CreateOrUpdateVendorInput input);
        Task DeleteVendor(EntityDto<int> input);
        Task<PagedResultDto<VendorDto>> GetVendor(GetVendorInput input);
        Task<GetVendorForEditOutput> GetVendorForEdit(EntityDto<int> input);
        GetVendorsListOutput GetVendorsList();
    }
}