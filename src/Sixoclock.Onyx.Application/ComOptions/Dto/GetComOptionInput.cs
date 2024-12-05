using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.ComOptions.Dto
{
    public class GetComOptionInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string Option { get; set; }
        public string Comment { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Option DESC";
            }
        }
    }
}
