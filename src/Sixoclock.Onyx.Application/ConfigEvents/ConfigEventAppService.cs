using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ConfigEvents.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ConfigEvents
{
    public class ConfigEventAppService : OnyxAppServiceBase, IConfigEventAppService
    {
        private readonly IRepository<ConfigEvent> _configEventRepository;
        public ConfigEventAppService(IRepository<ConfigEvent> configEventRepository)
        {
            _configEventRepository = configEventRepository;
        }
        public async Task<int> CreateOrUpdateConfigEvent(CreateOrUpdateConfigEventInput input)
        {
            var configEvent = ObjectMapper.Map<ConfigEvent>(input);

            if (input.Id == 0)
            {
                return await _configEventRepository.InsertAndGetIdAsync(configEvent);
            }
            else
            {
                await _configEventRepository.UpdateAsync(configEvent);
                return configEvent.Id;
            }
        }
        public async Task<GetConfigEventForEditOutput> GetConfigEventForEdit(EntityDto<int> input)
        {
            //Editing an existing configEvent
            var output = new GetConfigEventForEditOutput();
            if (input.Id == 0)
            {
                output.ConfigEvent = new ConfigEventDto();
            }
            else
            {
                var configEvent = await _configEventRepository.GetAsync(input.Id);

                output.ConfigEvent = ObjectMapper.Map<ConfigEventDto>(configEvent);
            }

            return output;
        }
        public GetConfigEventsListOutput GetConfigEventsList()
        {
            IEnumerable<ConfigEventListDto> _configEventsList = from configEvent in _configEventRepository.GetAll()
                                                              .Include(r => r.ConfigStatus)
                                                                select new ConfigEventListDto
                                                                {
                                                                    Id = configEvent.Id,
                                                                    Name = configEvent.ConfigStatus.Value
                                                                };
            return new GetConfigEventsListOutput { ConfigEvents = _configEventsList.ToList() };
        }
        public ListResultDto<ConfigEventDto> GetConfigEventsListByChargepoint(EntityDto<int> input)
        {
            var configEvents = from configEvent in _configEventRepository.GetAll()
                                                              .Include(r => r.ConfigStatus)
                                                              .Include(r => r.ConfigType)
                                                              .Include(r => r.OcppMessageEvent)
                                                              .Where(r => r.ChargepointId == input.Id)
                               select new ConfigEventDto
                               {
                                   Id = configEvent.Id,
                                   MessageEventResponse = configEvent.OcppMessageEvent.Response,
                                   ConfigStatusValue = configEvent.ConfigStatus.Value,
                                   ConfigStatusType = configEvent.ConfigType.Type,
                                   Date = configEvent.Date
                               };

            return new ListResultDto<ConfigEventDto>(ObjectMapper.Map<List<ConfigEventDto>>(configEvents));
        }
        public ListResultDto<ConfigEventDto> GetConfigEvent(GetConfigEventInput input)
        {
            var configEvents = from configEvent in _configEventRepository.GetAll()
                               select new ConfigEventDto
                               {
                                   Id = configEvent.Id,
                                   ConfigStatusValue = configEvent.ConfigStatus.Value,
                                   CreationTime = configEvent.CreationTime
                               };

            return new ListResultDto<ConfigEventDto>(ObjectMapper.Map<List<ConfigEventDto>>(configEvents));
        }
        public async Task DeleteConfigEvent(EntityDto<int> input)
        {
            var configEvent = await _configEventRepository.GetAsync(input.Id);
            await _configEventRepository.DeleteAsync(configEvent);
        }
    }
}
