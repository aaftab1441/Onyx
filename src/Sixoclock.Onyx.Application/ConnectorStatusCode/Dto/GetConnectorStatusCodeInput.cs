using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.ConnectorStatusCodes.Dto
{
    public class GetConnectorStatusCodeInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Status DESC";
            }
        }
    }
}
