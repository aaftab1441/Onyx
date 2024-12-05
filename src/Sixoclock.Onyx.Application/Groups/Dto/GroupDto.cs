using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Groups.Dto
{
    [AutoMapFrom(typeof(Group))]
    public class GroupDto : FullAuditedEntityDto, IHasCreationTime
    {
        public string GroupName { get; set; }
        public int TenantId { get; set; }
        public int InstallId { get; set; }
        public string InstallName { get; set; }
        public string Comment { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
