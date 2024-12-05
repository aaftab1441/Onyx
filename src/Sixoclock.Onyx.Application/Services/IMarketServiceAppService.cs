using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface IMarketServiceAppService : IApplicationService
    {
       
        Task<GetServiceListOutput> GetMarketServices(EntityDto<int> marketId);
        Task CreateOrUpdateService(CreateOrUpdateMarketServiceInputDto input);
        Task<GetMarketServiceForEditOutput> GetMarketServiceForEdit(EntityDto<int> input);
        Task DeleteService(EntityDto<int> input);

    }
}
