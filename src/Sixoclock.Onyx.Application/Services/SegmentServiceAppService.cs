using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public class SegmentServiceAppService:OnyxAppServiceBase,ISegmentServiceAppService
    {
        private readonly IRepository<SegmentService> _segmentServiceRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServicePriceParameter> _servicePriceParameteRepository;
        private readonly IRepository<SegmentServicePriceParameter> _segmentServicePriceParameter;

        public SegmentServiceAppService(IRepository<SegmentService> segmentServiceRepository, IRepository<Service> serviceRepository, IRepository<ServicePriceParameter> servicePriceParameteRepository, IRepository<SegmentServicePriceParameter> segmentServicePriceParameter)
        {
            _segmentServiceRepository = segmentServiceRepository;
            _serviceRepository = serviceRepository;
            _servicePriceParameteRepository = servicePriceParameteRepository;
            _segmentServicePriceParameter = segmentServicePriceParameter;
        }

        public async Task<GetServiceListOutput> GetSegmentServices(EntityDto<int> segmentId)
        {
            var segmentServices = await _segmentServiceRepository.GetAll().Include(x=>x.Service).Include(x=>x.SegmentServicePriceParameters).Where(x => x.SegmentId == segmentId.Id).Select(x => new ServiceDto() {Id = x.Id, CreationTime = x.CreationTime, Description = x.Service.Description, Name = x.Service.Name, PricingParamsCount = x.SegmentServicePriceParameters.Count}).ToListAsync();
            return new GetServiceListOutput(){ Services = segmentServices};
        }

        public async Task CreateOrUpdateService(CreateOrUpdateSegmentServiceInputDto input)
        {
            var segmentServiceId=await _segmentServiceRepository.InsertAndGetIdAsync(new SegmentService()
            {
                SegmentId = input.SegmentId,
                ServiceId = input.ServiceId
            });
            var parameters = await _servicePriceParameteRepository.GetAll().Where(x => x.ServiceId == input.ServiceId)
                .ToListAsync();
            foreach (var servicePriceParameter in parameters)
            {
               await  _segmentServicePriceParameter.InsertAsync(new SegmentServicePriceParameter()
                {
                    SegmentServiceId = segmentServiceId,
                    Name = servicePriceParameter.Name,
                    Value = servicePriceParameter.Value
                });
            }

        }

        public async Task<GetSegmentServiceForEditOutput> GetSegmentServiceForEdit(EntityDto<int> input)
        {
            var baseServices = await _serviceRepository.GetAll()
                .Select(x => new ComboboxItemDto(x.Id.ToString(), x.Name)).ToListAsync();
            baseServices.Insert(0,new ComboboxItemDto("0",""));
            return new GetSegmentServiceForEditOutput(){ BaseServices = baseServices};
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _segmentServiceRepository.DeleteAsync(input.Id);
        }

        
    }
}
