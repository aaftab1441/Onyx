using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Capacities.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.Capacities
{
    public class CapacityAppService : OnyxAppServiceBase, ICapacityAppService
    {
        private readonly IRepository<Capacity> _capacityRepository;
        public CapacityAppService(IRepository<Capacity> capacityRepository)
        {
            _capacityRepository = capacityRepository;
        }
        public async Task CreateOrUpdateCapacity(CreateOrUpdateCapacityInput input)
        {
            var capacity = ObjectMapper.Map<Capacity>(input);

            if (input.Id == 0)
            {
                await _capacityRepository.InsertAsync(capacity);
            }
            else
            {
                await _capacityRepository.UpdateAsync(capacity);
            }
        }
        public async Task<GetCapacityForEditOutput> GetCapacityForEdit(EntityDto<int> input)
        {
            //Editing an existing Capacity
            var output = new GetCapacityForEditOutput();
            if (input.Id == 0)
            {
                output.Capacity = new CapacityDto();
            }
            else
            {
                var Capacity = await _capacityRepository.GetAsync(input.Id);

                output.Capacity = ObjectMapper.Map<CapacityDto>(Capacity);
            }

            return output;
        }
        public GetCapacitiesListOutput GetCapacitiesList()
        {
            IEnumerable<CapacityListDto> _capacitiesList = from capacity in _capacityRepository.GetAll()
                                                                    select new CapacityListDto
                                                                    {
                                                                        Id = capacity.Id,
                                                                        Name = capacity.Value
                                                                    };
            return new GetCapacitiesListOutput { Capacities = _capacitiesList.ToList() };
        }

        public async Task<PagedResultDto<CapacityDto>> GetCapacity(GetCapacityInput input)
        {
            var query = (from capacity in _capacityRepository.GetAll()
                             .Include(c => c.Power)
                             .Include(c => c.Unit)
                         select new CapacityDto
                         {
                             Id = capacity.Id,
                             Value = capacity.Value,
                             Comment = capacity.Comment,
                             Power = capacity.Power.PowerName,
                             PowerId = capacity.PowerId,
                             UnitId = capacity.UnitId,
                             Unit = capacity.Unit.UnitName,
                             CreationTime = capacity.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Value.Contains(input.Filter))
                .WhereIf(input.UnitId.HasValue && input.UnitId > 0, item => item.UnitId == input.UnitId.Value)
                .WhereIf(input.PowerId.HasValue && input.PowerId > 0, item => item.PowerId == input.PowerId.Value)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<CapacityDto>(resultCount, results.ToList());
        }
        public async Task DeleteCapacity(EntityDto<int> input)
        {
            var capacity = await _capacityRepository.GetAsync(input.Id);
            await _capacityRepository.DeleteAsync(capacity);
        }
    }
}
