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
    public class CustomerServiceAppService : OnyxAppServiceBase, ICustomerServiceAppService
    {
        private readonly IRepository<CustomerService> _customerServiceRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServicePriceParameter> _servicePriceParameteRepository;
        private readonly IRepository<CustomerServicePriceParameter> _customerServicePriceParameter;

        public CustomerServiceAppService(IRepository<CustomerService> customerServiceRepository, IRepository<Service> serviceRepository, IRepository<ServicePriceParameter> servicePriceParameteRepository, IRepository<CustomerServicePriceParameter> customerServicePriceParameter)
        {
            _customerServiceRepository = customerServiceRepository;
            _serviceRepository = serviceRepository;
            _servicePriceParameteRepository = servicePriceParameteRepository;
            _customerServicePriceParameter = customerServicePriceParameter;

        }

        public async Task<GetServiceListOutput> GetCustomerServices(EntityDto<int> customerId)
        {
            var customerServices = await _customerServiceRepository.GetAll().Include(x => x.Service).Include(x => x.CustomerServicePriceParameters).Where(x => x.CustomerId == customerId.Id).Select(x => new ServiceDto() { Id = x.Id, CreationTime = x.CreationTime, Description = x.Service.Description, Name = x.Service.Name, PricingParamsCount = x.CustomerServicePriceParameters.Count }).ToListAsync();
            return new GetServiceListOutput() { Services = customerServices };
        }

        public async Task CreateOrUpdateService(CreateOrUpdateCustomerServiceInputDto input)
        {
            var customerServiceId = await _customerServiceRepository.InsertAndGetIdAsync(new CustomerService()
            {
                CustomerId = input.CustomerId,
                ServiceId = input.ServiceId
            });
            var parameters = await _servicePriceParameteRepository.GetAll().Where(x => x.ServiceId == input.ServiceId)
               .ToListAsync();
            foreach (var servicePriceParameter in parameters)
            {
                await _customerServicePriceParameter.InsertAsync(new CustomerServicePriceParameter()
                {
                    CustomerServiceId = customerServiceId,
                    Name = servicePriceParameter.Name,
                    Value = servicePriceParameter.Value
                });
            }
        }

        public async Task<GetCustomerServiceForEditOutput> GetCustomerServiceForEdit(EntityDto<int> input)
        {
            var baseServices = await _serviceRepository.GetAll()
                .Select(x => new ComboboxItemDto(x.Id.ToString(), x.Name)).ToListAsync();
            baseServices.Insert(0, new ComboboxItemDto("0", ""));
            return new GetCustomerServiceForEditOutput() { BaseServices = baseServices };
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _customerServiceRepository.DeleteAsync(input.Id);
        }


    }
}
