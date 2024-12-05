
using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Regions.Dto
{
    public class GetRegionInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public string RegionName { get; set; }

        public string MarketName { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
