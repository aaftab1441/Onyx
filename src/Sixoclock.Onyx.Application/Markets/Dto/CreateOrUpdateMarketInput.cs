using Abp.AutoMapper;

namespace Sixoclock.Onyx.Markets.Dto
{
    [AutoMapTo(typeof(Market))]
    public class CreateOrUpdateMarketInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int CustomerId { get; set; }
        public string MarketName { get; set; }
    }
}
