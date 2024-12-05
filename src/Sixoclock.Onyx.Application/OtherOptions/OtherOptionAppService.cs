using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.OtherOptions.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.OtherOptions
{
    public class OtherOptionAppService : OnyxAppServiceBase, IOtherOptionAppService
    {
        private readonly IRepository<OtherOption> _otherOptionRepository;

        public OtherOptionAppService(IRepository<OtherOption> otherOptionRepository)
        {
            _otherOptionRepository = otherOptionRepository;
        }
        public async Task CreateOrUpdateOtherOption(CreateOrUpdateOtherOptionInput input)
        {
            var otherOption = ObjectMapper.Map<OtherOption>(input);

            if (input.Id == 0)
            {
                await _otherOptionRepository.InsertAsync(otherOption);
            }
            else
            {
                await _otherOptionRepository.UpdateAsync(otherOption);
            }
        }
        public async Task<GetOtherOptionForEditOutput> GetOtherOptionForEdit(EntityDto<int> input)
        {
            //Editing an existing other option
            var output = new GetOtherOptionForEditOutput();
            if (input.Id == 0)
            {
                output.OtherOption = new OtherOptionDto();
            }
            else
            {
                var otherOption = await _otherOptionRepository.GetAsync(input.Id);

                output.OtherOption = ObjectMapper.Map<OtherOptionDto>(otherOption);
            }

            return output;
        }

        public async Task<PagedResultDto<OtherOptionDto>> GetOtherOption(GetOtherOptionInput input)
        {
            var query = (from other in _otherOptionRepository.GetAll()
                         select new OtherOptionDto
                         {
                             Id = other.Id,
                             Comment = other.Comment,
                             CreationTime = other.CreationTime,
                             TenantId = other.TenantId,
                             Option = other.Option
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Option.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Option.IsNullOrWhiteSpace(), item => item.Option == input.Option)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<OtherOptionDto>(resultCount, results.ToList());
        }
        public async Task DeleteOtherOption(EntityDto<int> input)
        {
            var otherOption = await _otherOptionRepository.GetAsync(input.Id);
            await _otherOptionRepository.DeleteAsync(otherOption);
        }
    }
}
