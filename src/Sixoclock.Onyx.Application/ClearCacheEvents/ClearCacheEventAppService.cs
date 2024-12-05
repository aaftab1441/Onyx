using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ClearCacheEvents.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ClearCacheEvents
{
    public class ClearCacheEventAppService : OnyxAppServiceBase, IClearCacheEventAppService
    {
        private readonly IRepository<ClearCacheEvent> _clearCacheEventRepository;
        public ClearCacheEventAppService(IRepository<ClearCacheEvent> clearCacheEventRepository)
        {
            _clearCacheEventRepository = clearCacheEventRepository;
        }
        public async Task<int> CreateOrUpdateClearCacheEvent(CreateOrUpdateClearCacheEventInput input)
        {
            var clearCacheEvent = ObjectMapper.Map<ClearCacheEvent>(input);

            if (input.Id == 0)
            {
                return await _clearCacheEventRepository.InsertAndGetIdAsync(clearCacheEvent);
            }
            else
            {
                await _clearCacheEventRepository.UpdateAsync(clearCacheEvent);
                return clearCacheEvent.Id;
            }
        }
        public async Task<GetClearCacheEventForEditOutput> GetClearCacheEventForEdit(EntityDto<int> input)
        {
            //Editing an existing clearCacheEvent
            var output = new GetClearCacheEventForEditOutput();
            if (input.Id == 0)
            {
                output.ClearCacheEvent = new ClearCacheEventDto();
            }
            else
            {
                var clearCacheEvent = await _clearCacheEventRepository.GetAsync(input.Id);

                output.ClearCacheEvent = ObjectMapper.Map<ClearCacheEventDto>(clearCacheEvent);
            }

            return output;
        }
        public GetClearCacheEventsListOutput GetClearCacheEventsList()
        {
            IEnumerable<ClearCacheEventListDto> _clearCacheEventsList = from clearCacheEvent in _clearCacheEventRepository.GetAll()
                                                              .Include(r => r.ClearCacheStatus)
                                                                        select new ClearCacheEventListDto
                                                                        {
                                                                            Id = clearCacheEvent.Id,
                                                                            Name = clearCacheEvent.ClearCacheStatus.Value
                                                                        };
            return new GetClearCacheEventsListOutput { ClearCacheEvents = _clearCacheEventsList.ToList() };
        }
        public ListResultDto<ClearCacheEventDto> GetClearCacheEventsListByChargepoint(EntityDto<int> input)
        {
            var clearCacheEvents = from clearCacheEvent in _clearCacheEventRepository.GetAll()
                                                              .Include(r => r.ClearCacheStatus)
                                                              .Include(r => r.OcppMessageEvent)
                                                              .Where(r => r.ChargepointId == input.Id)
                                   select new ClearCacheEventDto
                                   {
                                       Id = clearCacheEvent.Id,
                                       MessageEventResponse = clearCacheEvent.OcppMessageEvent.Response,
                                       ClearCacheStatusValue = clearCacheEvent.ClearCacheStatus.Value,
                                       Date = clearCacheEvent.Date
                                   };

            return new ListResultDto<ClearCacheEventDto>(ObjectMapper.Map<List<ClearCacheEventDto>>(clearCacheEvents));
        }
        public ListResultDto<ClearCacheEventDto> GetClearCacheEvent(GetClearCacheEventInput input)
        {
            var clearCacheEvents = from clearCacheEvent in _clearCacheEventRepository.GetAll()
                                   select new ClearCacheEventDto
                                   {
                                       Id = clearCacheEvent.Id,
                                       ClearCacheStatusValue = clearCacheEvent.ClearCacheStatus.Value,
                                       CreationTime = clearCacheEvent.CreationTime
                                   };

            return new ListResultDto<ClearCacheEventDto>(ObjectMapper.Map<List<ClearCacheEventDto>>(clearCacheEvents));
        }
        public async Task DeleteClearCacheEvent(EntityDto<int> input)
        {
            var clearCacheEvent = await _clearCacheEventRepository.GetAsync(input.Id);
            await _clearCacheEventRepository.DeleteAsync(clearCacheEvent);
        }
    }
}
