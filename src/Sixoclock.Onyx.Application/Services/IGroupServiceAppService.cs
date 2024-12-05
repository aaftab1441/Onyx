using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface IGroupServiceAppService : IApplicationService
    {
       
        Task<GetServiceListOutput> GetGroupServices(EntityDto<int> groupId);
        Task CreateOrUpdateService(CreateOrUpdateGroupServiceInputDto input);
        Task<GetGroupServiceForEditOutput> GetGroupServiceForEdit(EntityDto<int> input);
        Task DeleteService(EntityDto<int> input);

    }
}
