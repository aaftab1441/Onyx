using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Sixoclock.Onyx.Authorization.Users;

namespace Sixoclock.Onyx.Grants
{
    public class UserRuleSet:Entity
    {
        public int RuleSetId { get; set; }
        public RuleSet RuleSet { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
