using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    public class GetChargepointModelInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string ModelName { get; set; }
        public string Comment { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ModelName DESC";
            }
        }
    }
}
