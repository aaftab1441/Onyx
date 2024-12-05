using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Installs.Dto
{
    public class GetInstallInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string InstallName { get; set; }
        public string RegionName { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "InstallName DESC";
            }
        }
    }
}
