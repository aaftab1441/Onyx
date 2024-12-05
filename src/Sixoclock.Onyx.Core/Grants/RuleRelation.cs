using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Grants
{
    public class RuleRelation:Entity,IMustHaveTenant
    {
        public string Value { get; set; }
        public string Comment { get; set; }
        public int TenantId { get; set; }
    }
}
