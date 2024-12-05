using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sixoclock.Onyx.Grants
{
    public class Rule:Entity,IMustHaveTenant
    {
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public bool IsParanthesisStart { get; set; }
        public int RuleConditionId { get; set; }
        public int? RuleRelationId { get; set; }
        public int RuleSetId { get; set; }
        public int Order { get; set; }
        public int TenantId { get; set; }
        public RuleCondition RuleCondition { get; set; }
        public virtual RuleSet RuleSet { get; set; }
        public virtual RuleRelation RuleRelation { get; set; }
    }
}
