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
    public class ChargepointServiceAppService : OnyxAppServiceBase, IChargepointServiceAppService
    {
        private readonly IRepository<ChargepointService> _chargepointServiceRepository;
        private IRepository<Service> _serviceRepository;

        public ChargepointServiceAppService(IRepository<ChargepointService> chargepointServiceRepository, IRepository<Service> serviceRepository)
        {
            _chargepointServiceRepository = chargepointServiceRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<GetServiceListOutput> GetChargepointServices(EntityDto<int> chargepointId)
        {
            var chargepointServices = await _chargepointServiceRepository.GetAll().Include(x=>x.Service).Include(x=>x.ChargepointServicePriceParameters).Where(x => x.ChargepointId == chargepointId.Id).Select(x => new ServiceDto() {Id = x.Id, CreationTime = x.CreationTime, Description = x.Service.Description, Name = x.Service.Name, PricingParamsCount = x.ChargepointServicePriceParameters.Count}).ToListAsync();
            return new GetServiceListOutput(){ Services = chargepointServices };
        }
         
        public async Task CreateOrUpdateService(CreateOrUpdateChargepointServiceInputDto input)
        {
            await _chargepointServiceRepository.InsertAsync(new ChargepointService()
            {
                ChargepointId = input.ChargepointId,
                ServiceId = input.ServiceId
            });
        }

        public async Task<GetChargepointServiceForEditOutput> GetChargepointServiceForEdit(EntityDto<int> input)
        {
            var baseServices = await _serviceRepository.GetAll()
                .Select(x => new ComboboxItemDto(x.Id.ToString(), x.Name)).ToListAsync();
            return new GetChargepointServiceForEditOutput(){ BaseServices = baseServices};
        }

        public async Task DeleteService(EntityDto<int> input)
        {
            await _chargepointServiceRepository.DeleteAsync(input.Id);
        }

        
    }
}
