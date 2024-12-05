using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Transactions.Dto
{
    public class GetTransactionsByChargepointInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Id { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "EVSE_id DESC";
            }
        }
    }
}
