using Abp.AutoMapper;
using Sixoclock.Onyx.VendorErrorCodes.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.VendorErrorCodes
{
    [AutoMapFrom(typeof(GetVendorErrorCodeForEditOutput))]
    public class CreateOrEditVendorErrorCodeViewModel : GetVendorErrorCodeForEditOutput
    {
        public CreateOrEditVendorErrorCodeViewModel(GetVendorErrorCodeForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
