using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface IRegionServiceAppService : IApplicationService
    {
       
        Task<GetServiceListOutput> GetRegionServices(EntityDto<int> regionId);
        Task CreateOrUpdateService(CreateOrUpdateRegionServiceInputDto input);
        Task<GetRegionServiceForEditOutput> GetRegionServiceForEdit(EntityDto<int> input);
        Task DeleteService(EntityDto<int> input);

    }
}
