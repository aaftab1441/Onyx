using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;

namespace Sixoclock.Onyx.Grants.Domain
{
    public interface IRuleSetEntitiesProvider:IDomainService
    {
        Dictionary<string, IEnumerable<Tuple<string, string>>> GetRuleSetEntities();
        string GetRulablePropertyName(string entityName, string propertyAttribute);
    }
}
