using Abp.AutoMapper;
using Sixoclock.Onyx.Vendors.Dto;

namespace Sixoclock.Onyx.Web.Areas.App.Models.Vendors
{
    [AutoMapFrom(typeof(GetVendorForEditOutput))]
    public class CreateOrEditVendorViewModel : GetVendorForEditOutput
    {
        public CreateOrEditVendorViewModel(GetVendorForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}
