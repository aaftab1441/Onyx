using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Sixoclock.Onyx.Grants.Domain;

namespace Sixoclock.Onyx
{
    public interface IRegionRuleSetExpressionBuilder
    {
        Task<ExpressionStarter<Region>> BuiExpressionTree();
    }
}
