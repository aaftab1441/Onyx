using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.AvailabilityEvents.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.AvailabilityEvents
{
    public class AvailabilityEventAppService : OnyxAppServiceBase, IAvailabilityEventAppService
    {
        private readonly IRepository<AvailabilityEvent> _availabilityEventRepository;
        public AvailabilityEventAppService(IRepository<AvailabilityEvent> availabilityEventRepository)
        {
            _availabilityEventRepository = availabilityEventRepository;
        }
        public async Task<int> CreateOrUpdateAvailabilityEvent(CreateOrUpdateAvailabilityEventInput input)
        {
            var availabilityEvent = ObjectMapper.Map<AvailabilityEvent>(input);

            if (input.Id == 0)
            {
                return await _availabilityEventRepository.InsertAndGetIdAsync(availabilityEvent);
            }
            else
            {
                await _availabilityEventRepository.UpdateAsync(availabilityEvent);
                return availabilityEvent.Id;
            }
        }
        public async Task<GetAvailabilityEventForEditOutput> GetAvailabilityEventForEdit(EntityDto<int> input)
        {
            //Editing an existing availabilityEvent
            var output = new GetAvailabilityEventForEditOutput();
            if (input.Id == 0)
            {
                output.AvailabilityEvent = new AvailabilityEventDto();
            }
            else
            {
                var availabilityEvent = await _availabilityEventRepository.GetAsync(input.Id);

                output.AvailabilityEvent = ObjectMapper.Map<AvailabilityEventDto>(availabilityEvent);
            }

            return output;
        }
        public GetAvailabilityEventsListOutput GetAvailabilityEventsList()
        {
            IEnumerable<AvailabilityEventListDto> _availabilityEventsList = from availabilityEvent in _availabilityEventRepository.GetAll()
                                                              .Include(r => r.AvailabilityStatus)
                                                                            select new AvailabilityEventListDto
                                                                            {
                                                                                Id = availabilityEvent.Id,
                                                                                Name = availabilityEvent.AvailabilityStatus.Value
                                                                            };
            return new GetAvailabilityEventsListOutput { AvailabilityEvents = _availabilityEventsList.ToList() };
        }
        public ListResultDto<AvailabilityEventDto> GetAvailabilityEventsListByChargepoint(EntityDto<int> input)
        {
            var availabilityEvents = from availabilityEvent in _availabilityEventRepository.GetAll()
                                     .Include(r => r.EVSE)
                                                              .Include(r => r.AvailabilityStatus)
                                                              .Include(r => r.AvailabilityType)
                                                              .Include(r => r.OcppMessageEvent)
                                                              .Where(r => r.EVSE.ChargepointId == input.Id)
                                     select new AvailabilityEventDto
                                     {
                                         Id = availabilityEvent.Id,
                                         MessageEventResponse = availabilityEvent.OcppMessageEvent.Response,
                                         AvailabilityStatusValue = availabilityEvent.AvailabilityStatus.Value,
                                         AvailabilityTypeValue = availabilityEvent.AvailabilityType.Value,
                                         Date = availabilityEvent.Date
                                     };

            return new ListResultDto<AvailabilityEventDto>(ObjectMapper.Map<List<AvailabilityEventDto>>(availabilityEvents));
        }
        public ListResultDto<AvailabilityEventDto> GetAvailabilityEvent(GetAvailabilityEventInput input)
        {
            var availabilityEvents = from availabilityEvent in _availabilityEventRepository.GetAll()
                                     select new AvailabilityEventDto
                                     {
                                         Id = availabilityEvent.Id,
                                         AvailabilityStatusValue = availabilityEvent.AvailabilityStatus.Value,
                                         CreationTime = availabilityEvent.CreationTime
                                     };

            return new ListResultDto<AvailabilityEventDto>(ObjectMapper.Map<List<AvailabilityEventDto>>(availabilityEvents));
        }
        public async Task DeleteAvailabilityEvent(EntityDto<int> input)
        {
            var availabilityEvent = await _availabilityEventRepository.GetAsync(input.Id);
            await _availabilityEventRepository.DeleteAsync(availabilityEvent);
        }
    }
}
