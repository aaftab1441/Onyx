using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.MeterTypes.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.MeterTypes
{
    public class MeterTypeAppService : OnyxAppServiceBase, IMeterTypeAppService
    {
        private readonly IRepository<MeterType> _meterTypeRepository;
        public MeterTypeAppService(IRepository<MeterType> meterTypeRepository)
        {
            _meterTypeRepository = meterTypeRepository;
        }
        public async Task CreateOrUpdateMeterType(CreateOrUpdateMeterTypeInput input)
        {
            var meterType = ObjectMapper.Map<MeterType>(input);

            if (input.Id == 0)
            {
                await _meterTypeRepository.InsertAsync(meterType);
            }
            else
            {
                await _meterTypeRepository.UpdateAsync(meterType);
            }
        }
        public async Task<GetMeterTypeForEditOutput> GetMeterTypeForEdit(EntityDto<int> input)
        {
            //Editing an existing meter type
            var output = new GetMeterTypeForEditOutput();
            if (input.Id == 0)
            {
                output.MeterType = new MeterTypeDto();
            }
            else
            {
                var meterType = await _meterTypeRepository.GetAsync(input.Id);

                output.MeterType = ObjectMapper.Map<MeterTypeDto>(meterType);
            }

            return output;
        }
        public GetMeterTypesListOutput GetMeterTypesList()
        {
            IEnumerable<MeterTypeListDto> _meterTypesList = from meterType in _meterTypeRepository.GetAll()
                                                            select new MeterTypeListDto
                                                            {
                                                                Id = meterType.Id,
                                                                Name = meterType.Type
                                                            };
            return new GetMeterTypesListOutput { MeterTypes = _meterTypesList.ToList() };
        }
        public async Task<PagedResultDto<MeterTypeDto>> GetMeterType(GetMeterTypeInput input)
        {
            var query = (from meterType in _meterTypeRepository.GetAll()
                         select new MeterTypeDto
                         {
                             Id = meterType.Id,
                             Type = meterType.Type,
                             Comment = meterType.Comment,
                             CreationTime = meterType.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Type.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Type.IsNullOrWhiteSpace(), item => item.Type == input.Type)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<MeterTypeDto>(resultCount, results.ToList());
        }
        public async Task DeleteMeterType(EntityDto<int> input)
        {
            var meterType = await _meterTypeRepository.GetAsync(input.Id);
            await _meterTypeRepository.DeleteAsync(meterType);
        }

    }
}
