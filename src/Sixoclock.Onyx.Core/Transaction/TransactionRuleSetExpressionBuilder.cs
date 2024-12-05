using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using LinqKit;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.Grants;
using Sixoclock.Onyx.Grants.Domain;

namespace Sixoclock.Onyx
{
    public class TransactionRuleSetExpressionBuilder:RuleSetExpressionBuilderBase,ITransactionRuleSetExpressionBuilder
    {
        private readonly IRuleSetEntitiesProvider _ruleSetEntitiesProvider;
        public TransactionRuleSetExpressionBuilder(IRepository<UserRuleSet> userRuleSetRepository, IRepository<Rule> ruleRepository, IAbpSession abpSession, IRuleSetEntitiesProvider ruleSetEntitiesProvider, UserManager userManager) : base(userRuleSetRepository, ruleRepository, abpSession, ruleSetEntitiesProvider, userManager)
        {
            _ruleSetEntitiesProvider = ruleSetEntitiesProvider;
        }

        public async Task<ExpressionStarter<Transaction>> BuiExpressionTree()
        {

            var ruleSets = await GetAllRuleSets();
            var predicate = PredicateBuilder.New<Transaction>(true);
            foreach (var ruleSet in ruleSets)
            {
                await InterlBuildExpressionTree(ruleSet.Rules.ToList(), predicate, false);
            }
            return predicate;
        }
        private async Task InterlBuildExpressionTree(List<Rule> rules, ExpressionStarter<Transaction> expression, bool isOptional, Rule parentRule = null)
        {

            if (rules.Count > 0)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    var currentRule = rules[i];

                    if (currentRule.IsParanthesisStart && (parentRule == null || parentRule != currentRule))
                    {
                        var ruleEnd = GetRuleEnd(rules, i);
                        if (ruleEnd == currentRule)
                        {
                            await SetSinglePredicate(currentRule, expression, isOptional);
                        }
                        else
                        {
                            var innerPredicate = PredicateBuilder.New<Transaction>();

                            var ruleEndIndex = rules.IndexOf(ruleEnd);
                            await InterlBuildExpressionTree(rules.GetRange(i, ruleEndIndex - i + 1), innerPredicate, isOptional, currentRule);
                            if (ruleEnd != null && ruleEnd.RuleRelation.Value.Contains("AND"))
                            {
                                expression.And(innerPredicate);
                            }
                            else
                            {
                                expression.Or(innerPredicate);
                            }
                        }


                    }
                    else
                    {
                        await SetSinglePredicate(currentRule, expression, isOptional);
                    }
                    isOptional =
                        currentRule.RuleRelation == null ||
                        !currentRule.RuleRelation.Value.Contains("AND");
                }
            }
        }
        private async Task SetSinglePredicate(Rule rule, ExpressionStarter<Transaction> expression, bool isOptional)
        {
            string entity = rule.EntityName;
            string propertyName = _ruleSetEntitiesProvider.GetRulablePropertyName(entity, rule.PropertyName);
            if (string.IsNullOrEmpty(propertyName))
                return;
            string op = rule.RuleCondition.Value;
            string value = rule.Value;
            if (value.StartsWith('[') && value.EndsWith(']'))
                value = await GetVariableRuleValueAsync(value);
            switch (op)
            {
                case "=":
                    if (isOptional)
                        SetSingleEqualOrPredicate(entity, expression, propertyName, value);
                    else
                        SetSingleEqualAndPredicate(entity, expression, propertyName, value);
                    break;
                case "!=":
                    if (isOptional)
                        SetSingleNotEqualOrPredicate(entity, expression, propertyName, value);
                    else
                        SetSingleNotEqualAndPredicate(entity, expression, propertyName, value);
                    break;
                case "()":
                    if (isOptional)
                        SetSingleContainsOrPredicate(entity, expression, propertyName, value);
                    else
                        SetSingleContainsAndPredicate(entity, expression, propertyName, value);
                    break;
                case ">":
                    if (isOptional)
                        SetSingleLessThanOrPredicate(entity, expression, propertyName, value);
                    else
                        SetSingleLessThanAndPredicate(entity, expression, propertyName, value);
                    break;
                case "<":
                    if (isOptional)
                        SetSingleGreaterThanOrPredicate(entity, expression, propertyName, value);
                    else
                        SetSingleGreaterThanAndPredicate(entity, expression, propertyName, value);
                    break;
                default:
                    break;

            }

        }

        private void SetSingleEqualOrPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {
                        case "Duration":
                            if(DateTime.TryParse(value,out var date))
                            expression.Or(x => x.Duration.HasValue && x.Duration == date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.Or(x => x.Cost.HasValue && x.Cost == val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.Or(x => x.Earned.HasValue && x.Earned == earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.Or(x => x.ToBilled.HasValue && x.ToBilled == toBill);
                            break;
                        case "TransactionType":
                                expression.Or(x => x.TransactionType.Type==value);
                            break;
                        case "TransactionStatus":
                            expression.Or(x => x.TransactionStatus.Value == value);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.Or(x => x.TransactionStartTime.HasValue && x.TransactionStartTime == startTime);
                            break;
                        case "Reason":
                                expression.Or(x => x.Reason.ReasonName == value);
                            break;
                    }
                    break;




            }

        }
        private void SetSingleEqualAndPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {
                        case "Duration":
                            if (DateTime.TryParse(value, out var date))
                                expression.And(x => x.Duration.HasValue && x.Duration == date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.And(x => x.Cost.HasValue && x.Cost == val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.And(x => x.Earned.HasValue && x.Earned == earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.And(x => x.ToBilled.HasValue && x.ToBilled == toBill);
                            break;
                        case "TransactionType":
                            expression.And(x => x.TransactionType.Type == value);
                            break;
                        case "TransactionStatus":
                            expression.And(x => x.TransactionStatus.Value == value);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.And(x => x.TransactionStartTime.HasValue && x.TransactionStartTime == startTime);
                            break;
                        case "Reason":
                            expression.And(x => x.Reason.ReasonName == value);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleNotEqualOrPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {
                        case "Duration":
                            if (DateTime.TryParse(value, out var date))
                                expression.Or(x => x.Duration.HasValue && x.Duration != date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.Or(x => x.Cost.HasValue && x.Cost != val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.Or(x => x.Earned.HasValue && x.Earned != earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.Or(x => x.ToBilled.HasValue && x.ToBilled != toBill);
                            break;
                        case "TransactionType":
                            expression.Or(x => x.TransactionType.Type != value);
                            break;
                        case "TransactionStatus":
                            expression.Or(x => x.TransactionStatus.Value != value);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.Or(x => x.TransactionStartTime.HasValue && x.TransactionStartTime != startTime);
                            break;
                        case "Reason":
                            expression.Or(x => x.Reason.ReasonName != value);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleNotEqualAndPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {
                        case "Duration":
                            if (DateTime.TryParse(value, out var date))
                                expression.And(x => x.Duration.HasValue && x.Duration != date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.And(x => x.Cost.HasValue && x.Cost != val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.And(x => x.Earned.HasValue && x.Earned != earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.And(x => x.ToBilled.HasValue && x.ToBilled != toBill);
                            break;
                        case "TransactionType":
                            expression.And(x => x.TransactionType.Type != value);
                            break;
                        case "TransactionStatus":
                            expression.And(x => x.TransactionStatus.Value != value);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.And(x => x.TransactionStartTime.HasValue && x.TransactionStartTime != startTime);
                            break;
                        case "Reason":
                            expression.And(x => x.Reason.ReasonName != value);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleContainsOrPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {
                       
                        case "TransactionType":
                            expression.Or(x => x.TransactionType.Type.Contains(value));
                            break;
                        case "TransactionStatus":
                            expression.Or(x => x.TransactionStatus.Value.Contains(value));
                            break;
                        case "Reason":
                            expression.Or(x => x.Reason.ReasonName.Contains(value));
                            break;

                    }
                    break;


            }

        }
        private void SetSingleContainsAndPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {
                        case "TransactionType":
                            expression.And(x => x.TransactionType.Type.Contains(value));
                            break;
                        case "TransactionStatus":
                            expression.And(x => x.TransactionStatus.Value.Contains(value));
                            break;
                        case "Reason":
                            expression.And(x => x.Reason.ReasonName.Contains(value));
                            break;

                    }
                    break;


            }

        }
        private void SetSingleLessThanOrPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {

                        case "Duration":
                            if (DateTime.TryParse(value, out var date))
                                expression.Or(x => x.Duration.HasValue && x.Duration < date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.Or(x => x.Cost.HasValue && x.Cost < val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.Or(x => x.Earned.HasValue && x.Earned < earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.Or(x => x.ToBilled.HasValue && x.ToBilled < toBill);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.Or(x => x.TransactionStartTime.HasValue && x.TransactionStartTime < startTime);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleLessThanAndPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {

                        case "Duration":
                            if (DateTime.TryParse(value, out var date))
                                expression.And(x => x.Duration.HasValue && x.Duration < date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.And(x => x.Cost.HasValue && x.Cost < val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.And(x => x.Earned.HasValue && x.Earned < earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.And(x => x.ToBilled.HasValue && x.ToBilled < toBill);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.And(x => x.TransactionStartTime.HasValue && x.TransactionStartTime < startTime);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleGreaterThanOrPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {
                        case "Duration":
                            if (DateTime.TryParse(value, out var date))
                                expression.Or(x => x.Duration.HasValue && x.Duration > date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.Or(x => x.Cost.HasValue && x.Cost > val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.Or(x => x.Earned.HasValue && x.Earned > earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.Or(x => x.ToBilled.HasValue && x.ToBilled > toBill);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.Or(x => x.TransactionStartTime.HasValue && x.TransactionStartTime > startTime);
                            break;
                    }
                    break;

            }

        }
        private void SetSingleGreaterThanAndPredicate(string entity, ExpressionStarter<Transaction> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Transaction":
                    switch (propertyName)
                    {

                        case "Duration":
                            if (DateTime.TryParse(value, out var date))
                                expression.And(x => x.Duration.HasValue && x.Duration > date);
                            break;
                        case "Cost":
                            if (float.TryParse(value, out var val))
                                expression.And(x => x.Cost.HasValue && x.Cost > val);
                            break;
                        case "Earned":
                            if (float.TryParse(value, out var earned))
                                expression.And(x => x.Earned.HasValue && x.Earned > earned);
                            break;
                        case "ToBilled":
                            if (float.TryParse(value, out var toBill))
                                expression.And(x => x.ToBilled.HasValue && x.ToBilled > toBill);
                            break;
                        case "TransactionStartTime":
                            if (DateTime.TryParse(value, out var startTime))
                                expression.And(x => x.TransactionStartTime.HasValue && x.TransactionStartTime > startTime);
                            break;
                    }
                    break;

            }
        }
    }
}
