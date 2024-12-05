using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.ChargepointKeyValues.Dto
{
    public class GetChargepointKeyValueInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string ChargepointValue { get; set; }
        public string ModelName { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ModelName DESC";
            }
        }
    }
}
