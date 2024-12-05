using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.AdminStatuses.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.AdminStatuses
{
    public class AdminStatusAppService : OnyxAppServiceBase, IAdminStatusAppService
    {
        private readonly IRepository<AdminStatus> _adminStatusRepository;
        public AdminStatusAppService(IRepository<AdminStatus> adminStatusRepository)
        {
            _adminStatusRepository = adminStatusRepository;
        }
        public async Task CreateOrUpdateAdminStatus(CreateOrUpdateAdminStatusInput input)
        {
            var adminStatus = ObjectMapper.Map<AdminStatus>(input);

            if (input.Id == 0)
            {
                await _adminStatusRepository.InsertAsync(adminStatus);
            }
            else
            {
                await _adminStatusRepository.UpdateAsync(adminStatus);
            }
        }
        public async Task<GetAdminStatusForEditOutput> GetAdminStatusForEdit(EntityDto<int> input)
        {
            //Editing an existing Admin Status Code
            var output = new GetAdminStatusForEditOutput();
            if (input.Id == 0)
            {
                output.AdminStatus = new AdminStatusDto();
            }
            else
            {
                var adminStatus = await _adminStatusRepository.GetAsync(input.Id);

                output.AdminStatus = ObjectMapper.Map<AdminStatusDto>(adminStatus);
            }

            return output;
        }
        public GetAdminStatusesListOutput GetAdminStatusesList()
        {
            IEnumerable<AdminStatusListDto> _adminStatussList = from adminStatus in _adminStatusRepository.GetAll()
                                                                select new AdminStatusListDto
                                                                {
                                                                    Id = adminStatus.Id,
                                                                    Name = adminStatus.Status
                                                                };
            return new GetAdminStatusesListOutput { AdminStatuses = _adminStatussList.ToList() };
        }
        public async Task<PagedResultDto<AdminStatusDto>> GetAdminStatus(GetAdminStatusInput input)
        {
            var query = (from adminStatus in _adminStatusRepository.GetAll()
                         select new AdminStatusDto
                         {
                             Id = adminStatus.Id,
                             Status = adminStatus.Status,
                             Comment = adminStatus.Comment
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Status.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), item => item.Status == input.Status)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<AdminStatusDto>(resultCount, results.ToList());
        }
        public async Task DeleteAdminStatus(EntityDto<int> input)
        {
            var adminStatus = await _adminStatusRepository.GetAsync(input.Id);
            await _adminStatusRepository.DeleteAsync(adminStatus);
        }
    }
}
