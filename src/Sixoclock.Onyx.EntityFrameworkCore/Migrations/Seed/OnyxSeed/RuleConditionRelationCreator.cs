using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sixoclock.Onyx.EntityFrameworkCore;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class RuleConditionRelationCreator
    {
        public List<RuleRelation> InitialRuleConditionRelationType => GetInitialRuleConditionRelations();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<RuleRelation> GetInitialRuleConditionRelations()
        {
            return new List<RuleRelation>
            {
                new RuleRelation() { Value = "OR", Comment = "Rule Condition: OR", TenantId = _tenantId },
                new RuleRelation() { Value = "AND", Comment = "Rule Condition: AND", TenantId = _tenantId },
                new RuleRelation() { Value = "(", Comment = "Rule Condition: (", TenantId = _tenantId },
            new RuleRelation() { Value = ")", Comment = "Rule Condition: )", TenantId = _tenantId },
            new RuleRelation() { Value = ") OR", Comment = "Rule Condition: ) OR", TenantId = _tenantId },
            new RuleRelation() { Value = ") AND", Comment = "Rule Condition: ) AND", TenantId = _tenantId }
            };
        }

        public RuleConditionRelationCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRuleConditionRelation();
        }

        private void CreateRuleConditionRelation()
        {
            foreach (var ruleConditionRelation in InitialRuleConditionRelationType)
            {
                AddRuleConditionRelationIfNotExists(ruleConditionRelation);
            }
        }

        private void AddRuleConditionRelationIfNotExists(RuleRelation ruleRelation)
        {
            if (_context.RuleRelations.Any(l => l.TenantId == _tenantId && l.Value == ruleRelation.Value))
            {
                return;
            }
            _context.RuleRelations.Add(ruleRelation);

            _context.SaveChanges();
        }
    }
}
