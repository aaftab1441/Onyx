using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sixoclock.Onyx
{
    public static class ExpressionTreeBuilderExtension
    {
        //public static Expression<Func<Chargepoint, bool>>  BuildGrantExpression(List<ChargepointGrant> grants)
        //{
        //    var grant= grants.FirstOrDefault(x => x.GrantType.Value == "Rule");
        //    ParameterExpression param = Expression.Parameter(typeof(Chargepoint), "chargepoint");
        //    Expression final= null;
        //    foreach (var grantChargepointGrantRule in grant.ChargepointGrantRules)
        //    {
        //        Expression left=Expression.Property(param, typeof(Chargepoint).GetProperty(grantChargepointGrantRule.ColumnName));
        //        Expression right = Expression.Constant(grantChargepointGrantRule.Value,
        //            typeof(Chargepoint).GetProperty(grantChargepointGrantRule.ColumnName).PropertyType);
        //        Expression resulExpression=Expression.Equal(left,right);
        //        final=final!=null? Expression.A .ndAlso(final,resulExpression):resulExpression;
        //    }
        //    var expression = Expression.Lambda<Func<Chargepoint, bool>>(final, param);
        //    return expression;
        //}
    }
}
