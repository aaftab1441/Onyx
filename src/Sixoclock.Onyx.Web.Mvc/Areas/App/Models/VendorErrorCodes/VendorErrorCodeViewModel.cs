using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.VendorErrorCodes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.VendorErrorCodes
{
    [AutoMapFrom(typeof(ListResultDto<VendorErrorCodeDto>))]
    public class VendorErrorCodeViewModel : ListResultDto<VendorErrorCodeDto>
    {
        public VendorErrorCodeViewModel(ListResultDto<VendorErrorCodeDto> output)
        {
            output.MapTo(this);
        }
    }
}
