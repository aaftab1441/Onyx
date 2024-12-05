using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{

    public class InstallService : FullAuditedEntity
    {
        public int ServiceId { get; set; }
        public int InstallId { get; set; }
        public Install Install { get; set; }
        public Service Service { get; set; }
        public virtual ICollection<InstallServicePriceParameter> InstallServicePriceParameters { get; set; }
    }
}
