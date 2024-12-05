using Abp.AutoMapper;

namespace Sixoclock.Onyx.VendorErrorCodes.Dto
{
    [AutoMapTo(typeof(VendorErrorCode))]
    public class CreateOrUpdateVendorErrorCodeInput
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int VendorId { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public string Comment { get; set; }
    }
}
