using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    [AutoMapFrom(typeof(Customer))]
    public class CustomerDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string City { get; set; }
        public virtual string Phone1 { get; set; }
        public virtual string Phone2 { get; set; }
        public int SegmentId { get; set; }
        public string SegmentName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
