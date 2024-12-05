using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ParentTags.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ParentTags
{
    public class ParentTagAppService : OnyxAppServiceBase, IParentTagAppService
    {
        private readonly IRepository<ParentTag> _parentTagRepository;
        public ParentTagAppService(IRepository<ParentTag> parentTagRepository)
        {
            _parentTagRepository = parentTagRepository;
        }
        public async Task CreateOrUpdateParentTag(CreateOrUpdateParentTagInput input)
        {
            var parentTag = ObjectMapper.Map<ParentTag>(input);

            if (input.Id == 0)
            {
                await _parentTagRepository.InsertAsync(parentTag);
            }
            else
            {
                await _parentTagRepository.UpdateAsync(parentTag);
            }
        }
        public async Task<int> CreateOrUpdateParentTagAndGetId(CreateOrUpdateParentTagInput input)
        {
            var parentTag = ObjectMapper.Map<ParentTag>(input);

            if (input.Id == 0)
            {
                return await _parentTagRepository.InsertAndGetIdAsync(parentTag);
            }
            else
            {
                await _parentTagRepository.UpdateAsync(parentTag);
            }
            return parentTag.Id;
        }
        public async Task<GetParentTagForEditOutput> GetParentTagForEdit(EntityDto<int> input)
        {
            //Editing an existing ParentTag
            var output = new GetParentTagForEditOutput();
            if (input.Id == 0)
            {
                output.ParentTag = new ParentTagDto();
            }
            else
            {
                var parentTag = await _parentTagRepository.GetAsync(input.Id);

                output.ParentTag = ObjectMapper.Map<ParentTagDto>(parentTag);
            }

            return output;
        }
        public GetParentTagsListOutput GetParentTagsList()
        {
            IEnumerable<ParentTagListDto> _parentTagsList = from parentTag in _parentTagRepository.GetAll()
                                                        select new ParentTagListDto
                                                        {
                                                            Id = parentTag.Id,
                                                            Value = parentTag.Value
                                                        };
            return new GetParentTagsListOutput { ParentTags = _parentTagsList.ToList() };
        }
        public async Task<PagedResultDto<ParentTagDto>> GetParentTag(GetParentTagInput input)
        {
            var query = (from parentTag in _parentTagRepository.GetAll()
                         select new ParentTagDto
                         {
                             Id = parentTag.Id,
                             Value = parentTag.Value,
                             Comment = parentTag.Comment,
                             CreationTime = parentTag.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Value.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Value.IsNullOrWhiteSpace(), item => item.Value == input.Value)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<ParentTagDto>(resultCount, results.ToList());
        }
        public async Task DeleteParentTag(EntityDto<int> input)
        {
            var parentTag = await _parentTagRepository.GetAsync(input.Id);
            await _parentTagRepository.DeleteAsync(parentTag);
        }
    }
}
