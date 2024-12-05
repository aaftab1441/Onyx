using System.Collections.Generic;

namespace Sixoclock.Onyx.Vendors.Dto
{
    public class GetVendorsListOutput
    {
        public IEnumerable<VendorListDto> Vendors { get; set; }
    }
}
