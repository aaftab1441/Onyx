using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Capacities.Dto
{
    public class GetCapacityInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public int? UnitId { get; set; }
        public int? PowerId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Value DESC";
            }
        }
    }
}
