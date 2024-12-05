using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Groups.Dto
{
    public class GetGroupInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string GroupName { get; set; }
        public string InstallName { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "GroupName DESC";
            }
        }
    }
}
