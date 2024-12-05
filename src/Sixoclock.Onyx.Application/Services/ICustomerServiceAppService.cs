using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface ICustomerServiceAppService : IApplicationService
    {
       
        Task<GetServiceListOutput> GetCustomerServices(EntityDto<int> customertId);
        Task CreateOrUpdateService(CreateOrUpdateCustomerServiceInputDto input);
        Task<GetCustomerServiceForEditOutput> GetCustomerServiceForEdit(EntityDto<int> input);
        Task DeleteService(EntityDto<int> input);

    }
}
