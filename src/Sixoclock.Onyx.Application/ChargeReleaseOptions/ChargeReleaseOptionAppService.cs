using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ChargeReleaseOptions.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ChargeReleaseOptions
{
    public class ChargeReleaseOptionAppService : OnyxAppServiceBase, IChargeReleaseOptionAppService
    {
        private readonly IRepository<ChargeReleaseOption> _chargeReleaseOptionRepository;

        public ChargeReleaseOptionAppService(IRepository<ChargeReleaseOption> chargeReleaseOptionRepository)
        {
            _chargeReleaseOptionRepository = chargeReleaseOptionRepository;
        }
        public async Task CreateOrUpdateChargeReleaseOption(CreateOrUpdateChargeReleaseOptionInput input)
        {
            var chargeReleaseOption = ObjectMapper.Map<ChargeReleaseOption>(input);

            if (input.Id == 0)
            {
                await _chargeReleaseOptionRepository.InsertAsync(chargeReleaseOption);
            }
            else
            {
                await _chargeReleaseOptionRepository.UpdateAsync(chargeReleaseOption);
            }
        }
        public async Task<GetChargeReleaseOptionForEditOutput> GetChargeReleaseOptionForEdit(EntityDto<int> input)
        {
            //Editing an existing charge release option
            var output = new GetChargeReleaseOptionForEditOutput();
            if (input.Id == 0)
            {
                output.ChargeReleaseOption = new ChargeReleaseOptionDto();
            }
            else
            {
                var chargeReleaseOption = await _chargeReleaseOptionRepository.GetAsync(input.Id);
                                
                output.ChargeReleaseOption = ObjectMapper.Map<ChargeReleaseOptionDto>(chargeReleaseOption);
            }

            return output;
        }

        public async Task<PagedResultDto<ChargeReleaseOptionDto>> GetChargeReleaseOption(GetChargeReleaseOptionInput input)
        {
            var query = (from release in _chargeReleaseOptionRepository.GetAll()
                         select new ChargeReleaseOptionDto
                         {
                             Id = release.Id,
                             Comment = release.Comment,
                             CreationTime = release.CreationTime,
                             Option = release.Option,
                             TenantId = release.TenantId
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query                
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Option.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Option.IsNullOrEmpty(), item => item.Option == input.Option)
                .WhereIf(!input.Comment.IsNullOrEmpty(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ChargeReleaseOptionDto>(resultCount, results.ToList());
        }
        public async Task DeleteChargeReleaseOption(EntityDto<int> input)
        {
            var chargeReleaseOption = await _chargeReleaseOptionRepository.GetAsync(input.Id);
            await _chargeReleaseOptionRepository.DeleteAsync(chargeReleaseOption);
        }
    }
}
