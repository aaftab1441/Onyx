using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace Sixoclock.Onyx.MountTypes.Dto
{
    [AutoMapFrom(typeof(MountType))]
    public class MountTypeDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
