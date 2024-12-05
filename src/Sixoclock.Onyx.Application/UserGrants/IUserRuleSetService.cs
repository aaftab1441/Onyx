using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.UserGrants.Dto;

namespace Sixoclock.Onyx
{
    public interface IUserRuleSetService:IApplicationService
    {
        Task<UserRuleSetListOutput> GetUserRuleSets(EntityDto<long> userId);
        Task SetUserRuleSet(UserRuleSetInput userRuleSet);
        Task<GetRuleSetsByNameOutput> GetRuleSetByName(string name);
        Task DeleteUserRuleSet(EntityDto<int> id);

    }
}
