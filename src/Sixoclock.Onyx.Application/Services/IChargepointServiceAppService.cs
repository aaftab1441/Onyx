using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface IChargepointServiceAppService : IApplicationService
    {
       
        Task<GetServiceListOutput> GetChargepointServices(EntityDto<int> chargepointId);
        Task CreateOrUpdateService(CreateOrUpdateChargepointServiceInputDto input);
        Task<GetChargepointServiceForEditOutput> GetChargepointServiceForEdit(EntityDto<int> input);
        Task DeleteService(EntityDto<int> input);

    }
}
