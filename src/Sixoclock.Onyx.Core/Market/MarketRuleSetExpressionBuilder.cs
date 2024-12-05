﻿using System;
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
    public class MarketRuleSetExpressionBuilder:RuleSetExpressionBuilderBase,IMarketRuleSetExpressionBuilder
    {
        private readonly IRepository<Rule> _ruleRepository;
        private readonly IRepository<UserRuleSet> _userRuleSetRepository;
        private readonly IAbpSession _abpSession;
        private readonly IRuleSetEntitiesProvider _ruleSetEntitiesProvider;
        private async Task InterlBuildExpressionTree(List<Rule> rules, ExpressionStarter<Market> expression, bool isOptional, Rule parentRule = null)
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
                            var innerPredicate = PredicateBuilder.New<Market>();

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
        private async Task SetSinglePredicate(Rule rule, ExpressionStarter<Market> expression, bool isOptional)
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

        private void SetSingleEqualOrPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.Or(x => x.Customer.Segment.Name == value);
                    break;
                case "Customer":
                    expression.Or(x => x.Customer.CustomerName == value);
                    break;
                case "Market":
                    expression.Or(x => x.MarketName == value);
                    break;


            }

        }
        private void SetSingleEqualAndPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.And(x => x.Customer.Segment.Name == value);
                    break;
                case "Customer":
                    expression.And(x => x.Customer.CustomerName == value);
                    break;
                case "Market":
                    expression.And(x => x.MarketName == value);
                    break;

            }

        }
        private void SetSingleNotEqualOrPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.Or(x => x.Customer.Segment.Name != value);
                    break;
                case "Customer":
                    expression.Or(x => x.Customer.CustomerName != value);
                    break;
                case "Market":
                    expression.Or(x => x.MarketName != value);
                    break;


            }

        }
        private void SetSingleNotEqualAndPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.And(x => x.Customer.Segment.Name != value);
                    break;
                case "Customer":
                    expression.And(x => x.Customer.CustomerName != value);
                    break;
                case "Market":
                    expression.And(x => x.MarketName != value);
                    break;


            }

        }
        private void SetSingleContainsOrPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.Or(x => x.Customer.Segment.Name.Contains(value));
                    break;
                case "Customer":
                    expression.Or(x => x.Customer.CustomerName.Contains(value));
                    break;
                case "Market":
                    expression.Or(x => x.MarketName.Contains(value));
                    break;
  

            }

        }
        private void SetSingleContainsAndPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.And(x => x.Customer.Segment.Name.Contains(value));
                    break;
                case "Customer":
                    expression.And(x => x.Customer.CustomerName.Contains(value));
                    break;
                case "Market":
                    expression.And(x => x.MarketName.Contains(value));
                    break;


            }

        }
        private void SetSingleLessThanOrPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.Or(x => int.Parse(x.Customer.Segment.Name) < int.Parse(value));
                    break;
                case "Customer":
                    expression.Or(x => int.Parse(x.Customer.CustomerName) < int.Parse(value));
                    break;
                case "Market":
                    expression.Or(x => int.Parse(x.MarketName) < int.Parse(value));
                    break;



            }

        }
        private void SetSingleLessThanAndPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.And(x => int.Parse(x.Customer.Segment.Name) < int.Parse(value));
                    break;
                case "Customer":
                    expression.And(x => int.Parse(x.Customer.CustomerName) < int.Parse(value));
                    break;
                case "Market":
                    expression.And(x => int.Parse(x.MarketName) < int.Parse(value));
                    break;



            }

        }
        private void SetSingleGreaterThanOrPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.Or(x => int.Parse(x.Customer.Segment.Name) > int.Parse(value));
                    break;
                case "Customer":
                    expression.Or(x => int.Parse(x.Customer.CustomerName) > int.Parse(value));
                    break;
                case "Market":
                    expression.Or(x => int.Parse(x.MarketName) > int.Parse(value));
                    break;



            }

        }
        private void SetSingleGreaterThanAndPredicate(string entity, ExpressionStarter<Market> expression, string propertyName, string value)
        {
            switch (entity)
            {
                case "Segment":
                    expression.And(x => int.Parse(x.Customer.Segment.Name) > int.Parse(value));
                    break;
                case "Customer":
                    expression.And(x => int.Parse(x.Customer.CustomerName) > int.Parse(value));
                    break;
                case "Market":
                    expression.And(x => int.Parse(x.MarketName) > int.Parse(value));
                    break;

            }
        }


        public MarketRuleSetExpressionBuilder(IRepository<UserRuleSet> userRuleSetRepository,
            IRepository<Rule> ruleRepository, IAbpSession abpSession, IRuleSetEntitiesProvider ruleSetEntitiesProvider,
            UserManager userManager) : base(userRuleSetRepository, ruleRepository, abpSession, ruleSetEntitiesProvider,
            userManager)
        {
            _userRuleSetRepository = userRuleSetRepository;
            _ruleRepository = ruleRepository;
            _abpSession = abpSession;
            _ruleSetEntitiesProvider = ruleSetEntitiesProvider;
        }

        public async Task<ExpressionStarter<Market>> BuiExpressionTree()
        {

            var ruleSets = await GetAllRuleSets();
            var predicate = PredicateBuilder.New<Market>(true);
            foreach (var ruleSet in ruleSets)
            {
                await InterlBuildExpressionTree(ruleSet.Rules.ToList(), predicate, false);
            }
            return predicate;
        }
    }
}