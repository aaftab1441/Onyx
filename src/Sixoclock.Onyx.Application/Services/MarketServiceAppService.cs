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
    public class MarketServiceAppService:OnyxAppServiceBase, IMarketServiceAppService
    {
        private readonly IRepository<MarketService> _marketServiceRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServicePriceParameter> _servicePriceParameteRepository;
        private readonly IRepository<MarketServicePriceParameter> _marketServicePriceParameter;


        public MarketServiceAppService(IRepository<MarketService> marketServiceRepository, IRepository<Service> serviceRepository, IRepository<ServicePriceParameter> servicePriceParameteRepository, IRepository<MarketServicePriceParameter> marketServicePriceParameter)
        {
            _marketServiceRepository = marketServiceRepository;
            _serviceRepository = serviceRepository;
            _servicePriceParameteRepository = servicePriceParameteRepository;
            _marketServicePriceParameter = marketServicePriceParameter;

        }

        public async Task<GetServiceListOutput> GetMarketServices(EntityDto<int> marketId)
        {
            var marketServices = await _marketServiceRepository.GetAll().Include(x=>x.Service).Include(x=>x.MarketServicePriceParameters).Where(x => x.MarketId == marketId.Id).Select(x => new ServiceDto() {Id = x.Id, CreationTime = x.CreationTime, Description = x.Service.Description, Name = x.Service.Name, PricingParamsCount = x.MarketServicePriceParameters.Count}).ToListAsync();
            return new GetServiceListOutput(){ Services = marketServices };
        }

        public async Task CreateOrUpdateService(CreateOrUpdateMarketServiceInputDto input)
        {
            var marketServiceId = await _marketServiceRepository.InsertAndGetIdAsync(new MarketService()
            {
                MarketId = input.MarketId,
                ServiceId = input.ServiceId
            });
            var parameters = await _servicePriceParameteRepository.GetAll().Where(x => x.ServiceId == input.ServiceId)
               .ToListAsync();
            foreach (var servicePriceParameter in parameters)
            {
                await _marketServicePriceParameter.InsertAsync(new MarketServicePriceParameter()
                {
                    MarketServiceId = marketServiceId,
                    Name = servicePriceParameter.Name,
                    Value = servicePriceParameter.Value
                });
            }
        }

        public async Task<GetMarketServiceForEditOutput> GetMarketServiceForEdit(EntityDto<int> input)
        {
            var baseServices = await _serviceRepository.GetAll()
                .Select(x => new ComboboxItemDto(x.Id.ToString(), x.Name)).ToListAsync();
            baseServices.Insert(0, new ComboboxItemDto("0", ""));
            return new GetMarketServiceForEditOutput(){ BaseServices = baseServices};
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _marketServiceRepository.DeleteAsync(input.Id);
        }

        
    }
}
