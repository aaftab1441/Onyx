using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Markets.Dto
{
    public class GetMarketInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public string MarketName { get; set; }

        public string ClientName { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "MarketName DESC";
            }
        }
    }
}
