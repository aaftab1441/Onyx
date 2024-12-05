using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqKit;

namespace Sixoclock.Onyx
{
    public interface IEVSERuleSetExpressionBuilder
    {
        Task<ExpressionStarter<EVSE>> BuiExpressionTree();
    }
}
