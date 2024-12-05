using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.MountTypes.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.MountTypes
{
    public class MountTypeAppService : OnyxAppServiceBase, IMountTypeAppService
    {
        private readonly IRepository<MountType> _mountTypeRepository;
        public MountTypeAppService(IRepository<MountType> mountTypeRepository)
        {
            _mountTypeRepository = mountTypeRepository;
        }
        public async Task CreateOrUpdateMountType(CreateOrUpdateMountTypeInput input)
        {
            var mountType = ObjectMapper.Map<MountType>(input);

            if (input.Id == 0)
            {
                await _mountTypeRepository.InsertAsync(mountType);
            }
            else
            {
                await _mountTypeRepository.UpdateAsync(mountType);
            }
        }
        public async Task<GetMountTypeForEditOutput> GetMountTypeForEdit(EntityDto<int> input)
        {
            //Editing an existing Mount Type
            var output = new GetMountTypeForEditOutput();
            if (input.Id == 0)
            {
                output.MountType = new MountTypeDto();
            }
            else
            {
                var mountType = await _mountTypeRepository.GetAsync(input.Id);

                output.MountType = ObjectMapper.Map<MountTypeDto>(mountType);
            }

            return output;
        }
        public GetMountTypesListOutput GetMountTypesList()
        {
            IEnumerable<MountTypeListDto> _mountTypesList = from mountType in _mountTypeRepository.GetAll()
                                                      select new MountTypeListDto
                                                      {
                                                          Id = mountType.Id,
                                                          Name = mountType.Name
                                                      };
            return new GetMountTypesListOutput { MountTypes = _mountTypesList.ToList() };
        }
        public async Task<PagedResultDto<MountTypeDto>> GetMountType(GetMountTypeInput input)
        {
            var query = (from mountType in _mountTypeRepository.GetAll()
                         select new MountTypeDto
                         {
                             Id = mountType.Id,
                             Name = mountType.Name,
                             Comment = mountType.Comment,
                             CreationTime = mountType.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Name.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.Name == input.Name)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<MountTypeDto>(resultCount, results.ToList());
        }
        public async Task DeleteMountType(EntityDto<int> input)
        {
            var mountType = await _mountTypeRepository.GetAsync(input.Id);
            await _mountTypeRepository.DeleteAsync(mountType);
        }
    }
}
