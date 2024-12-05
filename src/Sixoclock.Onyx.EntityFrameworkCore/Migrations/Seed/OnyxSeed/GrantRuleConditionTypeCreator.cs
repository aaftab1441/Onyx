using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sixoclock.Onyx.EntityFrameworkCore;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class GrantRuleConditionTypeCreator
    {
        public List<RuleCondition> InitialGrantRuleConditionType => GetInitialGrantRuleConditionTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<RuleCondition> GetInitialGrantRuleConditionTypes()
        {
            return new List<RuleCondition>
            {
                new RuleCondition() { Value = "=", Comment = "Property value should contain value.", TenantId = _tenantId },
                new RuleCondition() { Value = "!=", Comment = "Property value should not contain value.", TenantId = _tenantId },
                new RuleCondition() { Value = ">", Comment = "Property value should be equal to value.", TenantId = _tenantId },
                new RuleCondition() { Value = "<", Comment = "Property value should not be equal to value.", TenantId = _tenantId },
                new RuleCondition() { Value = "()", Comment = "Property value should not be equal to value.", TenantId = _tenantId }
            };
        }

        public GrantRuleConditionTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateGrantRuleConditionTypes();
        }

        private void CreateGrantRuleConditionTypes()
        {
            foreach (var grantRuleConditionType in InitialGrantRuleConditionType)
            {
                AddGrantRuleConditionTypeIfNotExists(grantRuleConditionType);
            }
        }

        private void AddGrantRuleConditionTypeIfNotExists(RuleCondition grantRuleCondition)
        {
            if (_context.RuleConditions.Any(l => l.TenantId == _tenantId && l.Value == grantRuleCondition.Value))
            {
                return;
            }
            _context.RuleConditions.Add(grantRuleCondition);

            _context.SaveChanges();
        }
    }
}
