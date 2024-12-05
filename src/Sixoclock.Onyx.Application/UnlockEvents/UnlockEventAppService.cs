using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.UnlockEvents.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.UnlockEvents
{
    public class UnlockEventAppService : OnyxAppServiceBase, IUnlockEventAppService
    {
        private readonly IRepository<UnlockEvent> _unlockEventRepository;
        public UnlockEventAppService(IRepository<UnlockEvent> unlockEventRepository)
        {
            _unlockEventRepository = unlockEventRepository;
        }
        public async Task<int> CreateOrUpdateUnlockEvent(CreateOrUpdateUnlockEventInput input)
        {
            var unlockEvent = ObjectMapper.Map<UnlockEvent>(input);

            if (input.Id == 0)
            {
                return await _unlockEventRepository.InsertAndGetIdAsync(unlockEvent);
            }
            else
            {
                await _unlockEventRepository.UpdateAsync(unlockEvent);
                return unlockEvent.Id;
            }
        }
        public async Task<GetUnlockEventForEditOutput> GetUnlockEventForEdit(EntityDto<int> input)
        {
            //Editing an existing unlockEvent
            var output = new GetUnlockEventForEditOutput();
            if (input.Id == 0)
            {
                output.UnlockEvent = new UnlockEventDto();
            }
            else
            {
                var unlockEvent = await _unlockEventRepository.GetAsync(input.Id);

                output.UnlockEvent = ObjectMapper.Map<UnlockEventDto>(unlockEvent);
            }

            return output;
        }
        public GetUnlockEventsListOutput GetUnlockEventsList()
        {
            IEnumerable<UnlockEventListDto> _unlockEventsList = from unlockEvent in _unlockEventRepository.GetAll()
                                                              .Include(r => r.UnlockStatus)
                                                                select new UnlockEventListDto
                                                                {
                                                                    Id = unlockEvent.Id,
                                                                    Name = unlockEvent.UnlockStatus.Value
                                                                };
            return new GetUnlockEventsListOutput { UnlockEvents = _unlockEventsList.ToList() };
        }
        public ListResultDto<UnlockEventDto> GetUnlockEventsListByChargepoint(EntityDto<int> input)
        {
            var unlockEvents = from unlockEvent in _unlockEventRepository.GetAll()
                                                              .Include(r => r.EVSE)
                                                              .Include(r => r.UnlockStatus)
                                                              .Include(r => r.OcppMessageEvent)
                                                              .Where(r => r.EVSE.ChargepointId == input.Id)
                               select new UnlockEventDto
                               {
                                   Id = unlockEvent.Id,
                                   MessageEventResponse = unlockEvent.OcppMessageEvent.Response,
                                   UnlockStatusValue = unlockEvent.UnlockStatus.Value,
                                   EVSE_Id = unlockEvent.EVSE.EVSE_id,
                                   EVSEId = unlockEvent.EVSEId,
                                   Date = unlockEvent.Date
                               };

            return new ListResultDto<UnlockEventDto>(ObjectMapper.Map<List<UnlockEventDto>>(unlockEvents));
        }
        public ListResultDto<UnlockEventDto> GetUnlockEvent(GetUnlockEventInput input)
        {
            var unlockEvents = from unlockEvent in _unlockEventRepository.GetAll()
                               select new UnlockEventDto
                               {
                                   Id = unlockEvent.Id,
                                   Type = unlockEvent.UnlockStatus.Value,
                                   CreationTime = unlockEvent.CreationTime
                               };

            return new ListResultDto<UnlockEventDto>(ObjectMapper.Map<List<UnlockEventDto>>(unlockEvents));
        }
        public async Task DeleteUnlockEvent(EntityDto<int> input)
        {
            var unlockEvent = await _unlockEventRepository.GetAsync(input.Id);
            await _unlockEventRepository.DeleteAsync(unlockEvent);
        }
    }
}
