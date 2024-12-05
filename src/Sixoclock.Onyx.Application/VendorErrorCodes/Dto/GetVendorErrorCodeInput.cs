using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.VendorErrorCodes.Dto
{
    public class GetVendorErrorCodeInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public int ErrorCode { get; set; }

        public string VendorName { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
