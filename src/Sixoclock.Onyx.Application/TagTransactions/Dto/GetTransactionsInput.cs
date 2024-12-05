using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.TagTransactions.Dto
{
    public class GetTransactionsInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Id { get; set; }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Date DESC";
            }
        }
    }
}
