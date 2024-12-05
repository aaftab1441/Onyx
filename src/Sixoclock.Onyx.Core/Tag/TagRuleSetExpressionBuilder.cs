using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class TagRuleSetExpressionBuilder:RuleSetExpressionBuilderBase,ITagRuleSetExpressionBuilder
    {
        private readonly IRuleSetEntitiesProvider _ruleSetEntitiesProvider;
        public TagRuleSetExpressionBuilder(IRepository<UserRuleSet> userRuleSetRepository, IRepository<Rule> ruleRepository, IAbpSession abpSession, IRuleSetEntitiesProvider ruleSetEntitiesProvider, UserManager userManager) : base(userRuleSetRepository, ruleRepository, abpSession, ruleSetEntitiesProvider, userManager)
        {
            _ruleSetEntitiesProvider = ruleSetEntitiesProvider;
        }

        public async Task<ExpressionStarter<Tag>> BuiExpressionTree()
        {

            var ruleSets = await GetAllRuleSets();
            var predicate = PredicateBuilder.New<Tag>(true);
            foreach (var ruleSet in ruleSets)
            {
                await InterlBuildExpressionTree(ruleSet.Rules.ToList(), predicate, false);
            }
            return predicate;
        }
        private async Task InterlBuildExpressionTree(List<Rule> rules, ExpressionStarter<Tag> expression, bool isOptional, Rule parentRule = null)
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
                            var innerPredicate = PredicateBuilder.New<Tag>();

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
        private async Task SetSinglePredicate(Rule rule, ExpressionStarter<Tag> expression, bool isOptional)
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

        private void SetSingleEqualOrPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                        case "IdToken":
                            expression.Or(x => x.IdToken == value);
                            break;
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.Or(x => x.Expiry.HasValue && x.Expiry == datetime);
                            break;
                    }
                    break;
                    
                   


            }

        }
        private void SetSingleEqualAndPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                        case "IdToken":
                            expression.And(x => x.IdToken == value);
                            break;
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.And(x => x.Expiry.HasValue && x.Expiry == datetime);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleNotEqualOrPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                        case "IdToken":
                            expression.Or(x => x.IdToken != value);
                            break;
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.Or(x => x.Expiry.HasValue && x.Expiry != datetime);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleNotEqualAndPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                        case "IdToken":
                            expression.And(x => x.IdToken != value);
                            break;
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.And(x => x.Expiry.HasValue && x.Expiry != datetime);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleContainsOrPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                        case "IdToken":
                            expression.Or(x => x.IdToken.Contains(value));
                            break;
                      
                    }
                    break;


            }

        }
        private void SetSingleContainsAndPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                        case "IdToken":
                            expression.And(x => x.IdToken == value);
                            break;
                       
                    }
                    break;


            }

        }
        private void SetSingleLessThanOrPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                       
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.Or(x => x.Expiry.HasValue && x.Expiry < datetime);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleLessThanAndPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                      
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.And(x => x.Expiry.HasValue && x.Expiry < datetime);
                            break;
                    }
                    break;


            }

        }
        private void SetSingleGreaterThanOrPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.Or(x => x.Expiry.HasValue && x.Expiry > datetime);
                            break;
                    }
                    break;

            }

        }
        private void SetSingleGreaterThanAndPredicate(string entity, ExpressionStarter<Tag> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Tag":
                    switch (propertyName)
                    {
                       
                        case "Expiry":
                            if (DateTime.TryParse(value, out var datetime))
                                expression.And(x => x.Expiry.HasValue && x.Expiry > datetime);
                            break;
                    }
                    break;

            }
        }


    }
}
