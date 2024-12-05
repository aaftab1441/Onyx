using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx
{
    public class InstallServicePriceParameter:FullAuditedEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int InstallServiceId { get; set; }
        public InstallService InstallService { get; set; }
    }
}
