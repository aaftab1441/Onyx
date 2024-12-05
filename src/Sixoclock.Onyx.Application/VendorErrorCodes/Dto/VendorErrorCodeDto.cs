using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.VendorErrorCodes.Dto
{
    [AutoMapFrom(typeof(VendorErrorCode))]
    public class VendorErrorCodeDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public string Comment { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
}
