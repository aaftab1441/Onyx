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
    public class GroupServicePriceParameterAppService : OnyxAppServiceBase, IGroupServicePriceParameterAppService
    {
        private readonly IRepository<GroupServicePriceParameter> _groupServicePriceParametersRepository;
        private readonly List<string> _priceParameters;
        public GroupServicePriceParameterAppService(IRepository<GroupServicePriceParameter> groupServicePriceParametersRepository)
        {
            _groupServicePriceParametersRepository = groupServicePriceParametersRepository;
            _priceParameters = new List<string>
            {
                "PerUser","PerKwh", "Fixed", "PerChargepoint"
            };
        }

        public async Task<GetServicePriceParameterForEditOutput> GeServicePriceParameterForEdit(GetServicePriceParametersForEditParamInput<int> input)
        {
            if (input.Id > 0)
            {
                var serviceEntity = await _groupServicePriceParametersRepository.GetAsync(input.Id);
                return new GetServicePriceParameterForEditOutput()
                {
                    PriceParameter = new ServicePriceParameterDto() { Id = serviceEntity.Id, Name = serviceEntity.Name, ServiceId = serviceEntity.GroupServiceId, Value = serviceEntity.Value },
                    PriceParametersNames = _priceParameters.Select(x => new ComboboxItemDto(x, x)).ToList()

                };
            }
            else
            {
                return new GetServicePriceParameterForEditOutput()
                {
                    PriceParameter = new ServicePriceParameterDto() { Id = input.Id, ServiceId = input.ServiceId },
                    PriceParametersNames = _priceParameters.Select(x => new ComboboxItemDto(x, x)).ToList()
                };
            }
        }

        public async Task<GetServicePriceParameterListOutput> GetServicePriceParameters(int groupServiceId)
        {
            var priceParameters = await _groupServicePriceParametersRepository.GetAll()
                .Where(x => x.GroupServiceId == groupServiceId).Select(x =>
                    new ServicePriceParameterDto()
                    {
                        ServiceId = x.GroupServiceId,
                        Id = x.Id,
                        Name = x.Name,
                        Value = x.Value
                    }).ToListAsync();
            return new GetServicePriceParameterListOutput() { PriceParameters = priceParameters };
        }

        public async Task CreateOrUpdateServicePriceParameter(CreateOrUpdateServicePriceParameterInputDto input)
        {
            var entity = await _groupServicePriceParametersRepository.GetAsync(input.PriceParameter.Id);
            entity.Value = input.PriceParameter.Value;
            await _groupServicePriceParametersRepository.UpdateAsync(entity);
        }
    }
}
