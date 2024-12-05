using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.RemoteStartStopEvents.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.RemoteStartStopEvents
{
    public class RemoteStartStopEventAppService : OnyxAppServiceBase, IRemoteStartStopEventAppService
    {
        private readonly IRepository<RemoteStartStopEvent> _remoteStartStopEventRepository;
        public RemoteStartStopEventAppService(IRepository<RemoteStartStopEvent> remoteStartStopEventRepository)
        {
            _remoteStartStopEventRepository = remoteStartStopEventRepository;
        }
        public async Task<int> CreateOrUpdateRemoteStartStopEvent(CreateOrUpdateRemoteStartStopEventInput input)
        {
            var remoteStartStopEvent = ObjectMapper.Map<RemoteStartStopEvent>(input);

            if (input.Id == 0)
            {
                return await _remoteStartStopEventRepository.InsertAndGetIdAsync(remoteStartStopEvent);
            }
            else
            {
                await _remoteStartStopEventRepository.UpdateAsync(remoteStartStopEvent);
                return remoteStartStopEvent.Id;
            }
        }
        public async Task<GetRemoteStartStopEventForEditOutput> GetRemoteStartStopEventForEdit(EntityDto<int> input)
        {
            //Editing an existing remoteStartStopEvent
            var output = new GetRemoteStartStopEventForEditOutput();
            if (input.Id == 0)
            {
                output.RemoteStartStopEvent = new RemoteStartStopEventDto();
            }
            else
            {
                var remoteStartStopEvent = await _remoteStartStopEventRepository.GetAsync(input.Id);

                output.RemoteStartStopEvent = ObjectMapper.Map<RemoteStartStopEventDto>(remoteStartStopEvent);
            }

            return output;
        }
        public GetRemoteStartStopEventsListOutput GetRemoteStartStopEventsList()
        {
            IEnumerable<RemoteStartStopEventListDto> _remoteStartStopEventsList = from remoteStartStopEvent in _remoteStartStopEventRepository.GetAll()
                                                              .Include(r => r.RemoteStartStopEventType)
                                                                                  select new RemoteStartStopEventListDto
                                                                                  {
                                                                                      Id = remoteStartStopEvent.Id,
                                                                                      Name = remoteStartStopEvent.RemoteStartStopEventType.EventType
                                                                                  };
            return new GetRemoteStartStopEventsListOutput { RemoteStartStopEvents = _remoteStartStopEventsList.ToList() };
        }
        public ListResultDto<RemoteStartStopEventDto> GetRemoteStartStopEventsListByChargepoint(EntityDto<int> input)
        {
            var remoteStartStopEvents = from remoteStartStopEvent in _remoteStartStopEventRepository.GetAll()
                                                              .Include(r => r.EVSE)
                                                              .Include(r => r.RemoteStartStopEventType)
                                                              .Include(r => r.RemoteStartStopStatus)
                                                              .Include(r => r.OcppMessageEvent)
                                                              .Where(r => r.EVSE.ChargepointId == input.Id)
                                        select new RemoteStartStopEventDto
                                        {
                                            Id = remoteStartStopEvent.Id,
                                            EVSE_id = remoteStartStopEvent.EVSE.EVSE_id,
                                            MessageEventResponse = remoteStartStopEvent.OcppMessageEvent.Response,
                                            EventType = remoteStartStopEvent.RemoteStartStopEventType.EventType,
                                            StatusValue = remoteStartStopEvent.RemoteStartStopStatus.Value,
                                            CreationTime = remoteStartStopEvent.CreationTime
                                        };

            return new ListResultDto<RemoteStartStopEventDto>(ObjectMapper.Map<List<RemoteStartStopEventDto>>(remoteStartStopEvents));
        }
        public ListResultDto<RemoteStartStopEventDto> GetRemoteStartStopEvent(GetRemoteStartStopEventInput input)
        {
            var remoteStartStopEvents = from remoteStartStopEvent in _remoteStartStopEventRepository.GetAll()
                                        select new RemoteStartStopEventDto
                                        {
                                            Id = remoteStartStopEvent.Id,
                                            EventType = remoteStartStopEvent.RemoteStartStopEventType.EventType,
                                            CreationTime = remoteStartStopEvent.CreationTime
                                        };

            return new ListResultDto<RemoteStartStopEventDto>(ObjectMapper.Map<List<RemoteStartStopEventDto>>(remoteStartStopEvents));
        }
        public async Task DeleteRemoteStartStopEvent(EntityDto<int> input)
        {
            var remoteStartStopEvent = await _remoteStartStopEventRepository.GetAsync(input.Id);
            await _remoteStartStopEventRepository.DeleteAsync(remoteStartStopEvent);
        }
    }
}
