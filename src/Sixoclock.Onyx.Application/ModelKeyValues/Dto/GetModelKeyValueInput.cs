using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.ModelKeyValues.Dto
{
    public class GetModelKeyValueInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string ModelValue { get; set; }
        public string Comment { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
