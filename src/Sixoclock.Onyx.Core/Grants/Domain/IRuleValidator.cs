using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;

namespace Sixoclock.Onyx.Grants.Domain
{
    public interface IRuleValidator:IDomainService
    {
         bool Validate(List<Rule> rules);
         string Translate(List<Rule> rules);
    }
}
