using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public interface ISegmentServiceAppService:IApplicationService
    {
       
        Task<GetServiceListOutput> GetSegmentServices(EntityDto<int> segmentId);
        Task CreateOrUpdateService(CreateOrUpdateSegmentServiceInputDto input);
        Task<GetSegmentServiceForEditOutput> GetSegmentServiceForEdit(EntityDto<int> input);
        Task DeleteService(EntityDto<int> input);


    }
}
