using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Grants
{
    public class RuleSet:Entity,IMustHaveTenant
    {
        public string Name { get; set; }
        public string Translation { get; set; }
        public int TenantId { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
        public ICollection<UserRuleSet> UserRuleSets { get; set; }
    }
}
