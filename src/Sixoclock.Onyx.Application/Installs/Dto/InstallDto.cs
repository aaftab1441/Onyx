using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.Installs.Dto
{
    [AutoMapFrom(typeof(Install))]
    public class InstallDto : FullAuditedEntityDto, IHasCreationTime
    {
        public string InstallName { get; set; }
        public string Comment { get; set; }
        public int RegionId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int TenantId { get; set; }
        public string RegionName { get; set; }
    }
}
