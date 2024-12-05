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
    public class GroupServiceAppService : OnyxAppServiceBase, IGroupServiceAppService
    {
        private readonly IRepository<GroupService> _groupServiceRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServicePriceParameter> _servicePriceParameteRepository;
        private readonly IRepository<GroupServicePriceParameter> _groupServicePriceParameter;


        public GroupServiceAppService(IRepository<GroupService> groupServiceRepository, IRepository<Service> serviceRepository, IRepository<ServicePriceParameter> servicePriceParameteRepository, IRepository<GroupServicePriceParameter> groupServicePriceParameter)
        {
            _groupServiceRepository = groupServiceRepository;
            _serviceRepository = serviceRepository;
            _serviceRepository = serviceRepository;
            _servicePriceParameteRepository = servicePriceParameteRepository;
            _groupServicePriceParameter = groupServicePriceParameter;

        }

        public async Task<GetServiceListOutput> GetGroupServices(EntityDto<int> groupId)
        {
            var groupServices = await _groupServiceRepository.GetAll().Include(x=>x.Service).Include(x=>x.GroupServicePriceParameters).Where(x => x.GroupId == groupId.Id).Select(x => new ServiceDto() {Id = x.Id, CreationTime = x.CreationTime, Description = x.Service.Description, Name = x.Service.Name, PricingParamsCount = x.GroupServicePriceParameters.Count}).ToListAsync();
            return new GetServiceListOutput(){ Services = groupServices };
        }
         
        public async Task CreateOrUpdateService(CreateOrUpdateGroupServiceInputDto input)
        {
            var groupServiceId = await _groupServiceRepository.InsertAndGetIdAsync(new GroupService()
            {
                GroupId = input.GroupId,
                ServiceId = input.ServiceId
            });
            var parameters = await _servicePriceParameteRepository.GetAll().Where(x => x.ServiceId == input.ServiceId)
               .ToListAsync();
            foreach (var servicePriceParameter in parameters)
            {
                await _groupServicePriceParameter.InsertAsync(new GroupServicePriceParameter()
                {
                    GroupServiceId = groupServiceId,
                    Name = servicePriceParameter.Name,
                    Value = servicePriceParameter.Value
                });
            }

        }

        public async Task<GetGroupServiceForEditOutput> GetGroupServiceForEdit(EntityDto<int> input)
        {
            var baseServices = await _serviceRepository.GetAll()
                .Select(x => new ComboboxItemDto(x.Id.ToString(), x.Name)).ToListAsync();
            baseServices.Insert(0, new ComboboxItemDto("0", ""));

            return new GetGroupServiceForEditOutput(){ BaseServices = baseServices};
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _groupServiceRepository.DeleteAsync(input.Id);
        }

        
    }
}
