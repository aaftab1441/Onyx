using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sixoclock.Onyx.Vendors.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Vendors
{
    [AutoMapFrom(typeof(ListResultDto<VendorDto>))]
    public class VendorsViewModel : ListResultDto<VendorDto>
    {
        public VendorsViewModel(ListResultDto<VendorDto> output)
        {
            output.MapTo(this);
        }
    }
}
