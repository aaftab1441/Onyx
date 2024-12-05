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
    public class InstallServiceAppService : OnyxAppServiceBase, IInstallServiceAppService
    {
        private readonly IRepository<InstallService> _installServiceRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServicePriceParameter> _servicePriceParameteRepository;
        private readonly IRepository<InstallServicePriceParameter> _installServicePriceParameter;


        public InstallServiceAppService(IRepository<InstallService> installServiceRepository, IRepository<Service> serviceRepository, IRepository<ServicePriceParameter> servicePriceParameteRepository, IRepository<InstallServicePriceParameter> installServicePriceParameter)
        {
            _installServiceRepository = installServiceRepository;
            _serviceRepository = serviceRepository;
            _servicePriceParameteRepository = servicePriceParameteRepository;
            _installServicePriceParameter = installServicePriceParameter;

        }

        public async Task<GetServiceListOutput> GetInstallServices(EntityDto<int> installId)
        {
            var installServices = await _installServiceRepository.GetAll().Include(x=>x.Service).Include(x=>x.InstallServicePriceParameters).Where(x => x.InstallId == installId.Id).Select(x => new ServiceDto() {Id = x.Id, CreationTime = x.CreationTime, Description = x.Service.Description, Name = x.Service.Name, PricingParamsCount = x.InstallServicePriceParameters.Count}).ToListAsync();
            return new GetServiceListOutput(){ Services = installServices };
        }
         
        public async Task CreateOrUpdateService(CreateOrUpdateInstallServiceInputDto input)
        {
            var installServiceId = await _installServiceRepository.InsertAndGetIdAsync(new InstallService()
            {
                InstallId = input.InstallId,
                ServiceId = input.ServiceId
            });
            var parameters = await _servicePriceParameteRepository.GetAll().Where(x => x.ServiceId == input.ServiceId)
               .ToListAsync();
            foreach (var servicePriceParameter in parameters)
            {
                await _installServicePriceParameter.InsertAsync(new InstallServicePriceParameter()
                {
                    InstallServiceId = installServiceId,
                    Name = servicePriceParameter.Name,
                    Value = servicePriceParameter.Value
                });
            }

        }

        public async Task<GetInstallServiceForEditOutput> GetInstallServiceForEdit(EntityDto<int> input)
        {
            var baseServices = await _serviceRepository.GetAll()
                .Select(x => new ComboboxItemDto(x.Id.ToString(), x.Name)).ToListAsync();
            baseServices.Insert(0, new ComboboxItemDto("0", ""));

            return new GetInstallServiceForEditOutput(){ BaseServices = baseServices};
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _installServiceRepository.DeleteAsync(input.Id);
        }

        
    }
}
