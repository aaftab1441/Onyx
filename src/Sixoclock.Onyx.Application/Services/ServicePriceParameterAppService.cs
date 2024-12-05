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
    public class ServicePriceParameterAppService:OnyxAppServiceBase,IServicePriceParameterAppService
    {
        private readonly List<string> _priceParameters;
        private readonly IRepository<ServicePriceParameter> _servicePriceParameteRepository;

        public ServicePriceParameterAppService(IRepository<ServicePriceParameter> servicePriceParameteRepository)
        {
            _servicePriceParameteRepository = servicePriceParameteRepository;
            _priceParameters = new List<string>
            {
                "PerUser","PerKwh", "Fixed", "PerChargepoint"
            };
        }


        public async Task<GetServicePriceParameterForEditOutput> GeServicePriceParameterForEdit(GetServicePriceParametersForEditParamInput<int> input)
        {
            if (input.Id > 0)
            {
                var serviceEntity = await _servicePriceParameteRepository.GetAsync(input.Id);
                return new GetServicePriceParameterForEditOutput()
                {
                    PriceParameter = ObjectMapper.Map<ServicePriceParameterDto>(serviceEntity),
                    PriceParametersNames = _priceParameters.Select(x => new ComboboxItemDto(x, x)).ToList()
                    
                };
            }
            else
            {
                return new GetServicePriceParameterForEditOutput()
                {
                    PriceParameter = new ServicePriceParameterDto(){ Id = input.Id, ServiceId = input.ServiceId},
                    PriceParametersNames = _priceParameters.Select(x => new ComboboxItemDto(x, x)).ToList()
                };
            }
        }

        public async Task<GetServicePriceParameterListOutput> GetServicePriceParameters(int serviceId)
        {
            var data = await _servicePriceParameteRepository.GetAll().Where(x => x.ServiceId == serviceId)
                .Select(x => ObjectMapper.Map<ServicePriceParameterDto>(x)).ToListAsync();
            return new GetServicePriceParameterListOutput() {PriceParameters = data};
        }

        public async Task CreateOrUpdateServicePriceParameter(CreateOrUpdateServicePriceParameterInputDto input)
        {
            if (input.PriceParameter.Id > 0)
            {
                var entity = await _servicePriceParameteRepository.GetAsync(input.PriceParameter.Id);
                await _servicePriceParameteRepository.UpdateAsync(ObjectMapper.Map(input.PriceParameter, entity));
            }
            else
            {
                await _servicePriceParameteRepository.InsertAsync(ObjectMapper.Map<ServicePriceParameter>(input.PriceParameter));
            }
        }

        public async Task DeleteServicePriceParameter(EntityDto<int> input)
        {
            await _servicePriceParameteRepository.DeleteAsync(input.Id);
        }
    }
}
