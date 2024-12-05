using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Chargepoints.Dto
{
    public class GetChargepointInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string Place { get; set; }
        public string Comment { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Place DESC";
            }
        }
    }
}
