using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ResetEvents.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ResetEvents
{
    public class ResetEventAppService : OnyxAppServiceBase, IResetEventAppService
    {
        private readonly IRepository<ResetEvent> _resetEventRepository;
        public ResetEventAppService(IRepository<ResetEvent> resetEventRepository)
        {
            _resetEventRepository = resetEventRepository;
        }
        public async Task<int> CreateOrUpdateResetEvent(CreateOrUpdateResetEventInput input)
        {
            var resetEvent = ObjectMapper.Map<ResetEvent>(input);

            if (input.Id == 0)
            {
                return await _resetEventRepository.InsertAndGetIdAsync(resetEvent);
            }
            else
            {
                await _resetEventRepository.UpdateAsync(resetEvent);
                return resetEvent.Id;
            }
        }
        public async Task<GetResetEventForEditOutput> GetResetEventForEdit(EntityDto<int> input)
        {
            //Editing an existing resetEvent
            var output = new GetResetEventForEditOutput();
            if (input.Id == 0)
            {
                output.ResetEvent = new ResetEventDto();
            }
            else
            {
                var resetEvent = await _resetEventRepository.GetAsync(input.Id);

                output.ResetEvent = ObjectMapper.Map<ResetEventDto>(resetEvent);
            }

            return output;
        }
        public GetResetEventsListOutput GetResetEventsList()
        {
            IEnumerable<ResetEventListDto> _resetEventsList = from resetEvent in _resetEventRepository.GetAll()
                                                              .Include(r => r.ResetType)
                                                              select new ResetEventListDto
                                                              {
                                                                  Id = resetEvent.Id,
                                                                  Name = resetEvent.ResetType.Type
                                                              };
            return new GetResetEventsListOutput { ResetEvents = _resetEventsList.ToList() };
        }
        public ListResultDto<ResetEventDto> GetResetEventsListByChargepoint(EntityDto<int> input)
        {
            var resetEvents = from resetEvent in _resetEventRepository.GetAll()
                                                              .Where(r => r.ChargepointId == input.Id)
                                                              .Include(r => r.ResetType)
                                                              .Include(r => r.ResetStatus)
                                                              .Include(r => r.OcppMessageEvent)
                              select new ResetEventDto
                              {
                                  Id = resetEvent.Id,
                                  MessageEventResponse = resetEvent.OcppMessageEvent.Response,
                                  ResetType = resetEvent.ResetType.Type,
                                  ResetStatusValue = resetEvent.ResetStatus.ResetStatusValue,
                                  Date = resetEvent.Date
                              };

            return new ListResultDto<ResetEventDto>(ObjectMapper.Map<List<ResetEventDto>>(resetEvents));
        }
        public ListResultDto<ResetEventDto> GetResetEvent(GetResetEventInput input)
        {
            var resetEvents = from resetEvent in _resetEventRepository.GetAll()
                              select new ResetEventDto
                              {
                                  Id = resetEvent.Id,
                                  Type = resetEvent.ResetType.Type,
                                  CreationTime = resetEvent.CreationTime
                              };

            return new ListResultDto<ResetEventDto>(ObjectMapper.Map<List<ResetEventDto>>(resetEvents));
        }
        public async Task DeleteResetEvent(EntityDto<int> input)
        {
            var resetEvent = await _resetEventRepository.GetAsync(input.Id);
            await _resetEventRepository.DeleteAsync(resetEvent);
        }
    }
}
