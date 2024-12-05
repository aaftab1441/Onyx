using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface IServicesAppService:IApplicationService
    {
        Task<GetServiceForEditOutput> GetServiceForEdit(EntityDto<int> input);
        Task<GetServiceListOutput> GetServices();
        Task CreateOrUpdateService(CreateOrUpdateServiceInputDto input);
        Task DeleteService(EntityDto<int> input);
    }
}
