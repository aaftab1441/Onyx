using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sixoclock.Onyx
{
    public class LangDescription : FullAuditedEntity, IMustHaveTenant
    {
        public string LangShortnameInZero { get; set; }
        public int ChargepointModelId { get; set; }
        public int TenantId { get; set; }

        public virtual ChargepointModel ChargepointModel { get; set; }
    }
}
