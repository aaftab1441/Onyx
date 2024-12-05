using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.MeterValues.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.MeterValues
{
    public class MeterValueAppService : OnyxAppServiceBase, IMeterValueAppService
    {
        private readonly IRepository<MeterValue> _meterValueRepository;
        public MeterValueAppService(IRepository<MeterValue> meterValueRepository)
        {
            _meterValueRepository = meterValueRepository;
        }
        public async Task CreateOrUpdateMeterValue(CreateOrUpdateMeterValueInput input)
        {
            var meterValue = ObjectMapper.Map<MeterValue>(input);

            if (input.Id == 0)
            {
                await _meterValueRepository.InsertAsync(meterValue);
            }
            else
            {
                await _meterValueRepository.UpdateAsync(meterValue);
            }
        }
        public async Task<GetMeterValueForEditOutput> GetMeterValueForEdit(EntityDto<int> input)
        {
            //Editing an existing meter value
            var output = new GetMeterValueForEditOutput();
            if (input.Id == 0)
            {
                output.MeterValue = new MeterValueDto();
            }
            else
            {
                var meterValue = await _meterValueRepository.GetAsync(input.Id);

                output.MeterValue = ObjectMapper.Map<MeterValueDto>(meterValue);
            }

            return output;
        }
        public GetMeterValuesListOutput GetMeterValuesList()
        {
            IEnumerable<MeterValueListDto> _meterValuesList = from meterValue in _meterValueRepository.GetAll()
                                                      select new MeterValueListDto
                                                      {
                                                          Id = meterValue.Id,
                                                          Name = meterValue.MeterValueType.Type
                                                      };
            return new GetMeterValuesListOutput { MeterValues = _meterValuesList.ToList() };
        }
        public ListResultDto<MeterValueDto> GetMeterValueByTransaction(EntityDto<int> input)
        {
            var meterValues = from meterValue in _meterValueRepository.GetAll().Where(c => c.TransactionId == input.Id)
                                       //.Include(c => c.Transaction.EVSE.Capacity.Unit)
                                       .Include(c => c.MeterValueType)
                                       .Include(c => c.Measurand)
                                       .Include(c => c.Context)
                                       .Include(c => c.Location)
                                       .Include(c => c.Phase)
                                       .Include(c => c.Format)
                                       select new MeterValueDto
                                       {
                                           Id = meterValue.Id,
                                           MeterValue = meterValue.Value,
                                           //UnitName = meterValue.Transaction.EVSE.Capacity.Unit.UnitName,
                                           MeterValueType = meterValue.MeterValueType.Type,
                                           MeasurandType = meterValue.Measurand.MeasurandType,
                                           ContextName = meterValue.Context.ContextName,
                                           LocationName = meterValue.Location.LocationName,
                                           PhaseName = meterValue.Phase.PhaseName,
                                           FormatType = meterValue.Format.FormatType,
                                           MeterTime = meterValue.MeterTime,
                                           CreationTime = meterValue.CreationTime
                                       };

            return new ListResultDto<MeterValueDto>(ObjectMapper.Map<List<MeterValueDto>>(meterValues));
        }
        public async Task DeleteMeterValue(EntityDto<int> input)
        {
            var meterValue = await _meterValueRepository.GetAsync(input.Id);
            await _meterValueRepository.DeleteAsync(meterValue);
        }
    }
}
