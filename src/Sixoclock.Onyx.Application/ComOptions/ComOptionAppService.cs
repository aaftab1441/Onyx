using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ComOptions.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ComOptions
{
    public class ComOptionAppService : OnyxAppServiceBase, IComOptionAppService
    {
        private readonly IRepository<ComOption> _comOptionRepository;

        public ComOptionAppService(IRepository<ComOption> comOptionRepository)
        {
            _comOptionRepository = comOptionRepository;
        }
        public async Task CreateOrUpdateComOption(CreateOrUpdateComOptionInput input)
        {
            var comOption = ObjectMapper.Map<ComOption>(input);

            if (input.Id == 0)
            {
                await _comOptionRepository.InsertAsync(comOption);
            }
            else
            {
                await _comOptionRepository.UpdateAsync(comOption);
            }
        }
        public async Task<GetComOptionForEditOutput> GetComOptionForEdit(EntityDto<int> input)
        {
            //Editing an existing com option
            var output = new GetComOptionForEditOutput();
            if (input.Id == 0)
            {
                output.ComOption = new ComOptionDto();
            }
            else
            {
                var ComOption = await _comOptionRepository.GetAsync(input.Id);

                output.ComOption = ObjectMapper.Map<ComOptionDto>(ComOption);
            }

            return output;
        }

        public async Task<PagedResultDto<ComOptionDto>> GetComOption(GetComOptionInput input)
        {
            var query = (from com in _comOptionRepository.GetAll()
                         select new ComOptionDto
                         {
                             Id = com.Id,
                             Comment = com.Comment,
                             CreationTime = com.CreationTime,
                             Option = com.Option,
                             TenantId = com.TenantId
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Option.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Option.IsNullOrWhiteSpace(), item => item.Option == input.Option)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ComOptionDto>(resultCount, results.ToList());
        }
        public async Task DeleteComOption(EntityDto<int> input)
        {
            var comOption = await _comOptionRepository.GetAsync(input.Id);
            await _comOptionRepository.DeleteAsync(comOption);
        }
    }
}
