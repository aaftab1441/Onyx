using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Configs.Dto
{
    public class GetConfigInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public int VersionId { get; set; }
        public int FeatureId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "OCPPFeatureId DESC";
            }
        }
    }
}
