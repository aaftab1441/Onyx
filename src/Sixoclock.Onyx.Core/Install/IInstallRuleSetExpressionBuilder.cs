using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.Grants.Domain;

namespace Sixoclock.Onyx
{
    public interface IInstallRuleSetExpressionBuilder
    {
        Task<ExpressionStarter<Install>> BuiExpressionTree();
    }
}
