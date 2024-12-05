using Abp.AutoMapper;
using System;

namespace Sixoclock.Onyx.Tags.Dto
{
    [AutoMapTo(typeof(Tag))]
    public class CreateOrUpdateTagInput
    {
        public int Id { get; set;}
        public string IdToken { get; set; }
        public DateTime? Expiry { get; set; }
        public bool? ServiceContact { get; set; }
        public string Comment { get; set; }

        public int ParentTagId { get; set; }
        public int? AuthorizationStatusId { get; set; }
        public long? UserId { get; set; }
        public int TenantId { get; set; }
    }
}
