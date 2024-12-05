using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Editions;
using Sixoclock.Onyx.Services.Dto;

namespace Sixoclock.Onyx.Services
{
    public class ServicesAppService:OnyxAppServiceBase,IServicesAppService
    {
        private readonly IRepository<Service> _serviceRepository;
      


        public ServicesAppService(IRepository<Service> serviceRepository, IRepository<ServicePriceParameter> servicePriceParameteRepository)
        {
            _serviceRepository = serviceRepository;
           
        }

        public async Task<GetServiceForEditOutput> GetServiceForEdit(EntityDto<int> input)
        {
            var features = FeatureManager.GetAll();
            if (input.Id > 0)
            {
                var entity = await _serviceRepository.GetAsync(input.Id);
                return new GetServiceForEditOutput()
                {
                    Service = ObjectMapper.Map<ServiceDto>(entity)
                    ,
                    Features = features.Select(x => new ComboboxItemDto(x.Name, x.Name)).ToList()
                };
            }
            else
            {
                return new GetServiceForEditOutput()
                {
                    Service =new ServiceDto()
                    ,
                    Features = features.Select(x => new ComboboxItemDto(x.Name, x.Name)).ToList()
                };
            }
        }

        public async  Task<GetServiceListOutput> GetServices()
        {
            var services=await _serviceRepository.GetAll().Select(x=>new ServiceDto(){ Id=x.Id, Name = x.Name,FeatureName = x.FeatureName}).ToListAsync();
            return new GetServiceListOutput(){ Services = services};

        }

        public async Task CreateOrUpdateService(CreateOrUpdateServiceInputDto input)
        {
            if (input.Service.Id > 0)
            {
                var serivceEntity = await _serviceRepository.FirstOrDefaultAsync(input.Service.Id);
                serivceEntity.Name = input.Service.Name;
                serivceEntity.FeatureName = input.Service.FeatureName;
                await _serviceRepository.UpdateAsync(serivceEntity);
            }
            else
            {
                var serviceEntity=ObjectMapper.Map<Service>(input.Service);
                await _serviceRepository.InsertAsync(serviceEntity);

            }
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _serviceRepository.DeleteAsync(input.Id);

        }
    }
}
