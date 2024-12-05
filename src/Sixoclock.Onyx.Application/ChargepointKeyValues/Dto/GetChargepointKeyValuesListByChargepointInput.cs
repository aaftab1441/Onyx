using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.ChargepointKeyValues.Dto
{
    public class GetChargepointKeyValuesListByChargepointInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int ChargepointId { get; set; }
        public int TenantId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ChargepointValue DESC";
            }
        }
    }
}
