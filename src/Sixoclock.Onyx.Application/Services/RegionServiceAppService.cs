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
    public class RegionServiceAppService:OnyxAppServiceBase, IRegionServiceAppService
    {
        private readonly IRepository<RegionService> _regionServiceRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServicePriceParameter> _servicePriceParameteRepository;
        private readonly IRepository<RegionServicePriceParameter> _regionServicePriceParameter;


        public RegionServiceAppService(IRepository<RegionService> regionServiceRepository, IRepository<Service> serviceRepository, IRepository<ServicePriceParameter> servicePriceParameteRepository, IRepository<RegionServicePriceParameter> regionServicePriceParameter)
        {
            _regionServiceRepository = regionServiceRepository;
            _serviceRepository = serviceRepository;
            _servicePriceParameteRepository = servicePriceParameteRepository;
            _regionServicePriceParameter = regionServicePriceParameter;

        }

        public async Task<GetServiceListOutput> GetRegionServices(EntityDto<int> regionId)
        {
            var regionServices = await _regionServiceRepository.GetAll().Include(x=>x.Service).Include(x=>x.RegionServicePriceParameters).Where(x => x.RegionId == regionId.Id).Select(x => new ServiceDto() {Id = x.Id, CreationTime = x.CreationTime, Description = x.Service.Description, Name = x.Service.Name, PricingParamsCount = x.RegionServicePriceParameters.Count}).ToListAsync();
            return new GetServiceListOutput(){ Services = regionServices };
        }
         
        public async Task CreateOrUpdateService(CreateOrUpdateRegionServiceInputDto input)
        {
            var regionServiceId = await _regionServiceRepository.InsertAndGetIdAsync(new RegionService()
            {
                RegionId = input.RegionId,
                ServiceId = input.ServiceId
            });
            var parameters = await _servicePriceParameteRepository.GetAll().Where(x => x.ServiceId == input.ServiceId)
               .ToListAsync();
            foreach (var servicePriceParameter in parameters)
            {
                await _regionServicePriceParameter.InsertAsync(new RegionServicePriceParameter()
                {
                    RegionServiceId = regionServiceId,
                    Name = servicePriceParameter.Name,
                    Value = servicePriceParameter.Value
                });
            }
        }

        public async Task<GetRegionServiceForEditOutput> GetRegionServiceForEdit(EntityDto<int> input)
        {
            var baseServices = await _serviceRepository.GetAll()
                .Select(x => new ComboboxItemDto(x.Id.ToString(), x.Name)).ToListAsync();
            baseServices.Insert(0, new ComboboxItemDto("0", ""));

            return new GetRegionServiceForEditOutput(){ BaseServices = baseServices};
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _regionServiceRepository.DeleteAsync(input.Id);
        }

        
    }
}
