using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ElectricalOptions.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ElectricalOptions
{
    public class ElectricalOptionAppService : OnyxAppServiceBase, IElectricalOptionAppService
    {
        private readonly IRepository<ElectricalOption> _electricalOptionRepository;

        public ElectricalOptionAppService(IRepository<ElectricalOption> electricalOptionRepository)
        {
            _electricalOptionRepository = electricalOptionRepository;
        }
        public async Task CreateOrUpdateElectricalOption(CreateOrUpdateElectricalOptionInput input)
        {
            var electricalOption = ObjectMapper.Map<ElectricalOption>(input);

            if (input.Id == 0)
            {
                await _electricalOptionRepository.InsertAsync(electricalOption);
            }
            else
            {
                await _electricalOptionRepository.UpdateAsync(electricalOption);
            }
        }
        public async Task<GetElectricalOptionForEditOutput> GetElectricalOptionForEdit(EntityDto<int> input)
        {
            //Editing an existing electrical option
            var output = new GetElectricalOptionForEditOutput();
            if (input.Id == 0)
            {
                output.ElectricalOption = new ElectricalOptionDto();
            }
            else
            {
                var electricalOption = await _electricalOptionRepository.GetAsync(input.Id);

                output.ElectricalOption = ObjectMapper.Map<ElectricalOptionDto>(electricalOption);
            }

            return output;
        }

        public async Task<PagedResultDto<ElectricalOptionDto>> GetElectricalOption(GetElectricalOptionInput input)
        {
            var query = (from electrical in _electricalOptionRepository.GetAll()
                         select new ElectricalOptionDto
                         {
                             Id = electrical.Id,
                             Comment = electrical.Comment,
                             CreationTime = electrical.CreationTime,
                             Option = electrical.Option,
                             TenantId = electrical.TenantId
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Option.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Option.IsNullOrWhiteSpace(), item => item.Option == input.Option)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ElectricalOptionDto>(resultCount, results.ToList());
        }
        public async Task DeleteElectricalOption(EntityDto<int> input)
        {
            var electricalOption = await _electricalOptionRepository.GetAsync(input.Id);
            await _electricalOptionRepository.DeleteAsync(electricalOption);
        }
    }
}
