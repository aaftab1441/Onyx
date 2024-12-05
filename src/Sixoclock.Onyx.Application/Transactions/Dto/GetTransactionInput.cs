using Abp.Extensions;
using Abp.Runtime.Validation;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Transactions.Dto
{
    public class GetTransactionInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int CustomerId { get; set; }
        public int MarketId { get; set; }
        public int RegionId { get; set; }
        public int InstallId { get; set; }
        public int GroupId { get; set; }
        public int ChargepointId { get; set; }
        public int VendorId { get; set; }
        public int ModelId { get; set; }
        public int CapacityId { get; set; }
        public int EVSEId { get; set; }
        public int StatusId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
