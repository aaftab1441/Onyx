using Abp.AutoMapper;

namespace Sixoclock.Onyx.Vendors.Dto
{
    [AutoMapTo(typeof(Vendor))]
    public class CreateOrUpdateVendorInput
    {
        public int Id { get; set;}
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
