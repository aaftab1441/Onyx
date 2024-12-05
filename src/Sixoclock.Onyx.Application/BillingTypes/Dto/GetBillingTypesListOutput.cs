using System.Collections.Generic;

namespace Sixoclock.Onyx.BillingTypes.Dto
{
    public class GetBillingTypesListOutput
    {
        public IEnumerable<BillingTypeListDto> BillingTypes { get; set; }
    }
}
