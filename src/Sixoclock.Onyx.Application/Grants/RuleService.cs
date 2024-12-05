using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Grants.Domain;
using Sixoclock.Onyx.Grants.Dto;

namespace Sixoclock.Onyx.Grants
{
    public class RuleService:OnyxAppServiceBase,IRuleService
    {
        private readonly IRepository<RuleCondition> _ruleConditionRepository;
        private readonly IRepository<RuleRelation> _ruleRelationRepository;
        private readonly IRuleValidator _ruleValidator;
        private readonly IRuleSetEntitiesProvider _ruleSetEntitiesProvider;
        private readonly IRepository<Rule> _ruleRepository;
        private readonly IIocResolver _iocResolver;

        public RuleService(IRepository<RuleCondition> ruleConditionRepository, IRepository<RuleRelation> ruleRelationRepository, IRuleValidator ruleValidator, IRuleSetEntitiesProvider ruleSetEntitiesProvider, IRepository<Rule> ruleRepository, IIocResolver iocResolver)
        {
            _ruleConditionRepository = ruleConditionRepository;
            _ruleRelationRepository = ruleRelationRepository;
            _ruleValidator = ruleValidator;
            _ruleSetEntitiesProvider = ruleSetEntitiesProvider;
            _ruleRepository = ruleRepository;
            _iocResolver = iocResolver;
        }

        public async Task<GetRuleForEditOutputDto> GetRuleForEdit(GetRuleForEditParamInput<int> input)
        {
            var entities = _ruleSetEntitiesProvider.GetRuleSetEntities();
            List<PropertyDto> properties = new List<PropertyDto>();
            foreach (var entity in entities)
            {
                properties.AddRange(entity.Value.Select(x => new PropertyDto(){ Value = entity.Key,DisplayText = x.Item1, Type = x.Item2}));
            }
            var ruleRalations = (await _ruleRelationRepository.GetAllListAsync()).Select(x =>
                new ComboboxItemDto(x.Id.ToString(), x.Value)).ToList();
            ruleRalations.Insert(0,new ComboboxItemDto("0",""));
            List<ComboboxItemDto> ruleStartRelations = new List<ComboboxItemDto> {new ComboboxItemDto("0", "")};
            ruleStartRelations.AddRange(ruleRalations.Where(x => x.DisplayText == "("));

            if (input.Id > 0)
            {
              
                var rule = await _ruleRepository.GetAsync(input.Id);
                var ruleDto= ObjectMapper.Map<RuleDto>(rule);
                ruleDto.RuleEndRelationId = rule.RuleRelationId;
                ruleDto.RuleStartRelationId = rule.IsParanthesisStart
                    ? Int32.Parse(ruleStartRelations.FirstOrDefault(x => x.DisplayText == "(")?.Value)
                    : 0;
                return new GetRuleForEditOutputDto()
                {
                    Rule = ruleDto,
                    RuleOperators =
                        (await _ruleConditionRepository.GetAllListAsync()).Select(x =>
                            new ComboboxItemDto(x.Id.ToString(), x.Value)).ToList(),
                    RuleStartRelations = ruleStartRelations,
                    RuleEndRelations = ruleRalations,
                    Entities = entities.Select(x => new ComboboxItemDto(x.Key, x.Key)),
                    Properties = properties
                };

            }
            else
            {
                var ruleEntity = new GetRuleForEditOutputDto()
                {
                    Rule = new RuleDto() { Id = input.Id,  RuleSetId = input.RuleSetId },
                    RuleOperators =
                        (await _ruleConditionRepository.GetAllListAsync()).Select(x =>
                            new ComboboxItemDto(x.Id.ToString(), x.Value)).ToList(),
                    RuleStartRelations =ruleStartRelations,
                    RuleEndRelations = ruleRalations,
                    Entities = entities.Select(x=> new ComboboxItemDto(x.Key,x.Key)),
                    Properties = properties
                };

                

                return ruleEntity;

            }
        }

        //public Task<GetRuleListOutput> GetRules(int ruleSetId)
        //{
        //    var Entities = _ruleSetEntitiesProvider.GetRuleSetEntities();
        //    List<ComboboxItemDto> properties=new List<ComboboxItemDto>();
        //    foreach (var entity in Entities)
        //    {
        //       properties.AddRange(entity.Value.Select(x=>new ComboboxItemDto(entity.Key,x)));     
        //    }
        //    var rules = _ruleRepository.GetAll().Where(x => x.RuleSetId == ruleSetId).Include(x => x.RuleCondition)
        //        .Include(x => x.RuleRelation).Select(x =>
        //            new RuleListDto()
        //            {
        //                Id = x.Id,
        //                RelationStart = new ComboboxItemDto(),
        //                CreationTime = x.CreationTime,
        //                EntityName = x.EntityName,
        //                PropertyName = x.PropertyName,
        //                RuleCondition = new ComboboxItemDto(x.RuleConditionId.ToString(), x.RuleCondition.Value),
        //                RelationEnd = new ComboboxItemDto(x.RuleRelationId.ToString(), x.RuleRelation.Value),
        //                Value = x.Value,
        //                Entities = Entities.Select(u => u.Key),
        //                Properties = properties,
        //                Conditions = _ruleConditionRepository.GetAll().Select(u=>new ComboboxItemDto(u.Id.ToString(),u.Value)),
        //                Relations = _ruleRelationRepository.GetAll().Select(u=>new ComboboxItemDto(u.Id.ToString(),u.Value))



        //            });
        //    return Task.FromResult(new GetRuleListOutput(){ Rules = rules.ToList()});
        //}
        public Task<GetRuleListOutput> GetRules(int ruleSetId)
        {
           
            var rules = _ruleRepository.GetAll().Where(x => x.RuleSetId == ruleSetId).Include(x => x.RuleCondition)
                .Include(x => x.RuleRelation).OrderBy(x=>x.Order).Select(x =>
                    new RuleListDto()
                    {
                        Id = x.Id,
                        RelationStart = x.IsParanthesisStart? "(":"",
                        EntityName = x.EntityName,
                        PropertyName = x.PropertyName,
                        RuleCondition =x.RuleCondition.Value,
                        RelationEnd =x.RuleRelation.Value,
                        Value = x.Value,
                    });
            return Task.FromResult(new GetRuleListOutput()
            {
                Rules = rules.ToList()

            });
           
        }

        public async Task CreateOrUpdateRule(CreateOrUpdateRuleInputDto input)
        {
            var ruleRalation = (await _ruleRelationRepository.GetAllListAsync())
                .FirstOrDefault(x => x.Value == "(");
           
            if (input.Rule?.Id > 0)
            {

                var rule = await _ruleRepository.FirstOrDefaultAsync(input.Rule.Id);
                 rule =  ObjectMapper.Map(input.Rule, rule);
                if (ruleRalation != null)
                    rule.IsParanthesisStart = input.Rule.RuleStartRelationId.HasValue &&
                                              input.Rule.RuleStartRelationId == ruleRalation.Id;
                rule.RuleRelationId = input.Rule.RuleEndRelationId>0?input.Rule.RuleEndRelationId:null;
                await _ruleRepository.UpdateAsync(rule);
            }
            else
            {
                var allRules = _ruleRepository.GetAll()
                    .Where(x => x.RuleSetId == input.Rule.RuleSetId);
                var rule = ObjectMapper.Map<Rule>(input.Rule);
            
                  
                rule.Order = allRules.Any()? allRules.Max(x=>x.Order) + 1:1;
                if (ruleRalation != null)
                    rule.IsParanthesisStart = input.Rule.RuleStartRelationId.HasValue &&
                                              input.Rule.RuleStartRelationId == ruleRalation.Id;
                rule.RuleRelationId = input.Rule.RuleEndRelationId>0?input.Rule.RuleEndRelationId:null;
                await _ruleRepository.InsertAsync(rule);
            }
        }

        public async Task DeleteRule(EntityDto<int> input)
        {
            var order = (await _ruleRepository.GetAsync(input.Id)).Order;
            await _ruleRepository.DeleteAsync(input.Id);
            var rulesWithHigherOrder = _ruleRepository.GetAll().Where(x => x.Order > order);
            foreach (var rule in rulesWithHigherOrder)
            {
                rule.Order--;
                await _ruleRepository.UpdateAsync(rule);
            }
        }
    
        public Task<List<PropertyDto>> GetProperties(string entityName)
        {
            var entities = _ruleSetEntitiesProvider.GetRuleSetEntities();
            List<PropertyDto> properties = new List<PropertyDto>();
            foreach (var entity in entities.Where(x=>x.Key==entityName))
            {
                properties.AddRange(entity.Value.Select(x => new PropertyDto(){ DisplayText = x.Item1,Type = x.Item2,Value = entity.Key}));
            }
            return Task.FromResult(properties);
        }

        public  Task<List<ComboboxItemDto>> GetPropertyValues(string propertyType)
        {
            var values = new List<ComboboxItemDto>();
            if (propertyType == typeof(AdminStatus).Name)
            {
                using (var repo=_iocResolver.ResolveAsDisposable<IRepository<AdminStatus>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.Status)).ToList();
                }
            }
            else if (propertyType == typeof(Currency).Name)
            {
                using (var repo = _iocResolver.ResolveAsDisposable<IRepository<Currency>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.Value)).ToList();
                }
            }
            else if (propertyType == typeof(BillingType).Name)
            {
                using (var repo = _iocResolver.ResolveAsDisposable<IRepository<BillingType>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.Type)).ToList();
                }
            }
            else if (propertyType == typeof(BillingStatus).Name)
            {
                using (var repo = _iocResolver.ResolveAsDisposable<IRepository<BillingStatus>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.Value)).ToList();
                }
            }
            else if (propertyType == typeof(Reason).Name)
            {
                using (var repo = _iocResolver.ResolveAsDisposable<IRepository<Reason>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.ReasonName)).ToList();
                }
            }
            else if (propertyType == typeof(TransactionStatus).Name)
            {
                using (var repo = _iocResolver.ResolveAsDisposable<IRepository<TransactionStatus>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.Value)).ToList();
                }
            }
            else if (propertyType == typeof(TransactionType).Name)
            {
                using (var repo = _iocResolver.ResolveAsDisposable<IRepository<TransactionType>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.Type)).ToList();
                }
            }
            else if (propertyType==typeof(Country).Name)
            {
                using (var repo = _iocResolver.ResolveAsDisposable<IRepository<Country>>())
                {
                    values = repo.Object.GetAll().Select(x => new ComboboxItemDto(x.Id.ToString(), x.Value)).ToList();
                }
            }
            return Task.FromResult(values);
        }
    }
}
